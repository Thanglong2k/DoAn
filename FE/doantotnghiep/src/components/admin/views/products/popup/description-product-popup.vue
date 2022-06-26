<template>
  <el-dialog
    ref="dialog"
    title="Thêm mô tả sản phẩm"
    :visible.sync="modelDialog"
  >
    <template #default>
      <el-form v-model="modelForm">
        <el-form-item
            class="tw-flex-1 tw-mr-4"
            :label="$t('Product.Price')"
            prop="Price"
          >
            <el-input
              v-model="modelForm.Price"
              :placeholder="$t('Product.PriceInput')"
              @change="changeData"
            />
          </el-form-item>
      <el-form-item
            class="tw-flex-1 tw-mr-4"
            :label="$t('Product.PromotionPrice')"
            prop="PromotionPrice"
          >
            <el-input
              v-model="modelForm.PromotionPrice"
              :placeholder="$t('Product.PromotionPriceInput')"
              @change="changeData"
            />
          </el-form-item>
      <el-form-item
            class="tw-flex-1 tw-mr-4"
            :label="$t('Product.StartTimeDate')"
            prop="StartTimeDate"
          >
            <el-date-picker
      v-model="modelForm.StartTimeDate"
      type="date"
      :placeholder="$t('SelectDate')"
      @change="changeData"
      >
    </el-date-picker>
          </el-form-item>
      <el-form-item
            class="tw-flex-1 tw-mr-4"
            :label="$t('Product.EndTimeDate')"
            prop="EndTimeDate"
          >
            <el-date-picker
      v-model="modelForm.EndTimeDate"
      type="date"
      :placeholder="$t('SelectDate')"
      @change="changeData"
      >
    </el-date-picker>
          </el-form-item>

          <el-form-item>
              
              <VueEditor v-model="modelForm.Description"/>
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
import { VueEditor } from "vue2-editor";
export default {
    props:{
        productAttributes:{
      type:Array,
      default:()=>{}
    }
    },
    components:{
    VueEditor
  },
    data() {
    return {
      modelDialog: false,
      modelForm: {
        Price:null,
        PromotionPrice:null,
        Description:'',
        StartTimeDate:null,
        EndTimeDate:null
      },
      rules:{
        ProductName: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        ManufacturerID: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        CategoryID: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        Origin: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        ReleaseTime: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
      },
      description:'',
      price:null,
      pricePromotion:null,
      startDate:null,
      endDate:null,
    };
  },
  created(){
    const me=this
    me.initDataValue()
  },
  methods:{
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
        const model=[]
        model.push(me.modelForm)
        for(let i=0;i<model.length;i++){
          // model[i].StartTimeDate=new Date(model[i].StartTimeDate)
          // model[i].EndTimeDate=new Date(model[i].EndTimeDate)
          if(model[i].PromotionPrice){
            const h = this.$createElement;
            debugger
            if(!model[i].StartTimeDate||!model[i].EndTimeDate){
              
              me.$notify.warning({
                title: me.$t("Notify.Warning"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  "Hãy nhập thời gian khuyến mại"
                ),
              });
              me.$emit('updateError',true)
            }else if(new Date(model[i].StartTimeDate)>new Date(model[i].EndTimeDate)){
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
        me.$emit('updateProductAttributes',model)
    },
    /**
     * Hủy thêm thôn gtin chi tiết sản phẩm
     */
    cancelFill(){
        const me=this
        me.modelDialog = false;
        me.$emit('closePopup')
    },
    changeData(){
      
      
    },
    initDataValue(){
      const me=this
      const productAttribute= me.productAttributes?me.productAttributes[0]:{}
      if(productAttribute&&productAttribute.Price){
        me.modelForm.Price=productAttribute.Price
      }else{
        me.modelForm.Price= null
      }
      if(productAttribute&&productAttribute.PromotionPrice){
        me.modelForm.PromotionPrice=productAttribute.PromotionPrice
      }else{
        me.modelForm.PromotionPrice= null
      }
      if(productAttribute&&productAttribute.StartTimeDate){
        me.modelForm.StartTimeDate=productAttribute.StartTimeDate
      }else{
        me.modelForm.StartTimeDate= null
      }
      if(productAttribute&&productAttribute.EndTimeDate){
        me.modelForm.EndTimeDate=productAttribute.EndTimeDate
      }else{
        me.modelForm.EndTimeDate= null
      }
      if(productAttribute&&productAttribute.Description){
        me.modelForm.Description=productAttribute.Description
      }else{
        me.modelForm.Description= null
      }
    },
  }
}
</script>

<style lang="scss" scoped>
::v-deep{
  .ql-container{
    height:auto !important;
  }
}
</style>