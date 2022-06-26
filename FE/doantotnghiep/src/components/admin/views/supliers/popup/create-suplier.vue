<template>
  <el-dialog
    ref="dialog"
    :title="typePopup?'Thêm mới nhà cung cấp':'Sửa nhà cung cấp'"
    :visible.sync="modelDialog"
    width="500px"
    @close="cancelFill"
  >
    <template #default>
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="132px"
        label-position="left"
        class="demo-ruleForm"
      >
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          label="Tên nhà cung cấp"
          prop="SuplierName"
        >
          <el-input
            v-model="modelForm.SuplierName"
            placeholder="Nhập tên nhà cung cấp"
          />
        </el-form-item>
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          label="Số điện thoại"
          prop="PhoneNumber"
        >
          <el-input
            v-model="modelForm.PhoneNumber"
            placeholder="Nhập số điện thoại"
          />
        </el-form-item>
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          label="Email"
          prop="Email"
        >
          <el-input
            v-model="modelForm.Email"
            placeholder="Nhập Email"
          />
        </el-form-item>
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          label="Địa chỉ"
          prop="Address"
        >
          <el-input
            v-model="modelForm.Address"
            placeholder="Nhập địa chỉ"
          />
        </el-form-item>
      </el-form>
    </template>
    <template #footer>
      <div class="button">
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
import { mapActions } from "vuex";
export default {
    props:{
        suplier:{
            type:Object,
            default:()=>{}
        },
        typePopup:{
            type:Boolean,
            default:true
        }
    },
  data() {
    return {
      modelDialog: false,
      modelForm: {},

      rules: {},
      pageIndex:1,
      pageSize:10,
      search:''
    };
  },
created(){
    const me=this
    me.modelForm=JSON.parse(JSON.stringify(me.suplier))
},
  methods: {
    ...mapActions("suplier", ["insertSuplier","updateSuplier","getSupliersPaging"]),
    setModel(value) {
      const me = this;
      me.modelDialog = value;
    },
    /**
     * Hoàn thành thêm thông tin chi tiết sản phẩm
     */
    async completeFill() {
      const me = this;
      if(this.typePopup){

          
          const h = this.$createElement;
          await me.insertSuplier(me.modelForm).then((res) => {
              me.modelForm={}
              
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.InsertSuccess",{object:'nhà cung cấp'})
              ),
              type: "success",
            });
            me.getSupliers(me.pageIndex, me.pageSize, me.search);
          }).catch(()=>{
              
    
                this.$notify.error({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.HasError")
                  ),
                });
          });
      }else{
          const h = this.$createElement;
          await me.updateSuplier({params:me.modelForm,id:me.suplier.SuplierID}).then((res) => {
              me.modelForm={}
              
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.UpdateSuccess",{object:'nhà cung cấp'})
              ),
              type: "success",
            });
            me.getSupliers(me.pageIndex, me.pageSize, me.search);
          }).catch(()=>{
                this.$notify.error({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.HasError")
                  ),
                });
          });
      }

      me.modelDialog = false;
    },
    /**
     * Hủy thêm thôn gtin chi tiết sản phẩm
     */
    cancelFill() {
      const me = this;
      me.modelDialog = false;
      me.$emit('closePopup')
    },
    getSupliers(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText,
      };
      me.getSupliersPaging(params);
    },
  },
};
</script>

<style scoped>
.complete-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>