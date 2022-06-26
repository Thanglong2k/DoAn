<template>
  <el-dialog
    ref="dialog"
    title="Thêm IMEI"
    :visible.sync="modelDialog"
  >
    <template #default>
        <div class="form">
            <div class="label-text">Nhập IMEI</div>
            <div class="text-field">
                <el-input v-model="imei" @change="addIMEI"></el-input>
            </div>
        </div>
        <div class="imei-list">
            <div class="imei-item tw-flex tw-items-center" v-for="(item,index) in imeis" :key="index">
                <div class="imei-text">{{item}}</div>
                <el-button class="button-delete" @click="deleteEMEI(index)">X</el-button>
            </div>
        </div>
    </template>
    <template #footer>
      <div class="button tw-flex">
          <el-button @click="completeFill" class="complete-btn">{{
        $t("Button.Complete")
      }}</el-button>
          <el-button @click="cancelFill" class="cancel-btn">{{
        $t("Button.Cancel")
      }}</el-button>
      </div>
    </template>
  </el-dialog>
</template>

<script>
export default {
    props:{
        orderDetail:{

            type:Object,
            default:()=>{}
        }
    },
data() {
    return {
      modelDialog: false,
      modelForm: [],
      active:0,
      rules:{},
      imei:'',
      imeis:[]
    };
  },
  mounted(){
      const me=this
      me.imeis=JSON.parse(JSON.stringify(me.orderDetail.IMEI)).split(',')
  },
  methods: {
    setModel(value) {
      const me = this;
      me.modelDialog = value;
    },
    /**
     * Hoàn thành thêm thông tin chi tiết sản phẩm
     */
    completeFill(){
        const me=this
        me.modelDialog = false;
        me.$emit('updateIMEIValue',me.imeis,me.orderDetail.OrderDetailID)
        me.$emit('closePopup')
    },
    /**
     * Hủy thêm thôn gtin chi tiết sản phẩm
     */
    cancelFill(){
        const me=this
        me.modelDialog = false;
        me.$emit('closePopup')
    },
    addIMEI(){
        const me=this
        const isExists=me.imeis.indexOf(me.imei)>=0
        if(isExists){
            const h= me.$createElement
            this.$notify.error({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.ExistsIMEI")
                  ),
                });
        }else{
            if(me.imei!==''){

              me.imeis.unshift(me.imei)
              me.imei=''
            }
        }
    },
    deleteEMEI(index){
        const me=this
        me.imeis.splice(index,1)
    }
  }
}
</script>

<style scoped>
.button-delete{
  @apply tw-w-10 tw-mt-0 tw-ml-3 !important;
}
</style>