<template>
  <el-dialog
    ref="dialog"
    title="Thông tin chi tiết"
    :visible.sync="modelDialog"
  >
    <template #default>
      <div class="tabs">
        <el-button :class="{'bg-red':active===index}" v-for="(item,index) in modelForm" @click="active=index" :key="index">{{item.Memory}}</el-button>
        <div class="tw-mt-2" v-for="(item,index1) in modelForm" :key="`A${index1}`" >
          <bonus-info v-if="active===index1" :productAttribute="item" @updateModel="(model)=>updateModel(index1,model)" ></bonus-info>
        </div>
        <!-- <component :is="'BonusInfo'"/> -->
      </div>
      
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
import BonusInfo from '../bonus-info-form.vue'
export default {
  props:{
    productAttributes:{
      type:[Array,String],
      default:()=>{}
    }
  },
  components:{
BonusInfo
  },
data() {
    return {
      modelDialog: false,
      modelForm: [],
      active:0,
      rules:{},
    };
  },
  computed:{
    
  },
  created(){
const me=this
debugger
me.modelForm=JSON.parse(JSON.stringify(me.productAttributes))
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
        debugger
        for(let i=0;i<me.modelForm.length;i++){
          // me.modelForm[i].StartTimeDate=new Date(me.modelForm[i].StartTimeDate)
          // me.modelForm[i].EndTimeDate=new Date(me.modelForm[i].EndTimeDate)
          if(me.modelForm[i].PromotionPrice){
            const h = this.$createElement;
            debugger
            if(!me.modelForm[i].StartTimeDate||!me.modelForm[i].EndTimeDate){
              
              me.$notify.warning({
                title: me.$t("Notify.Warning"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  "Hãy nhập thời gian khuyến mại"
                ),
              });
              me.$emit('updateError',true)
            }else if(new Date(me.modelForm[i].StartTimeDate)>new Date(me.modelForm[i].EndTimeDate)){
              me.$notify.warning({
                title: me.$t("Notify.Warning"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  "Ngày kết thúc khuyến mại phải lớn hơn ngày bắt đầu khuyến mại"
                ),
              });
              me.$emit('updateError',true)
            }else{
              me.$emit('updateError',false)
            }
          }
        }
        me.$emit('closePopup')
        me.$emit('updateProductAttributes',me.modelForm)
    },
    /**
     * Hủy thêm thôn gtin chi tiết sản phẩm
     */
    cancelFill(){
        const me=this
        me.modelDialog = false;
        me.$emit('closePopup')
    },
    updateModel(index,model){
      this.modelForm[index].Description=model.Description
      this.modelForm[index].StartTimeDate=this.$commonFn.addHours(7,new Date(model.StartTimeDate))
      this.modelForm[index].EndTimeDate=this.$commonFn.addHours(7,new Date(model.EndTimeDate))
      this.modelForm[index].Price=model.Price
      this.modelForm[index].PromotionPrice=model.PromotionPrice
      // this.modelForm[index].RangeDate={}
      // if(model.StartTimeDate){
      //   //this.modelForm[index].RangeDate.StartDate=new Date()
      //   this.modelForm[index].RangeDate.StartDate=this.$commonFn.addHours(7,model.StartTimeDate)
      // }
      // if(model.EndTimeDate){
      //   //this.modelForm[index].RangeDate.EndDate=new Date()
      //   this.modelForm[index].RangeDate.EndDate=this.$commonFn.addHours(7,model.EndTimeDate)
      // }
      
    }
  },
  watch:{
    productAttributes(){
      const me=this
      me.modelForm=JSON.parse(JSON.stringify(me.productAttributes))
      debugger
    }
  }
}
</script>

<style scoped>
.complete-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
.bg-red{
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<style lang="scss" scoped>

::v-deep{
    .el-dialog{
        margin-top:6vh !important;
    }
}
</style>