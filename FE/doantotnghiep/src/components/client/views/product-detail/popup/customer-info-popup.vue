<template>
  <el-dialog
    ref="dialog"
    :title="title"
    :visible.sync="modelDialog"
  >
    <template #default>
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="120px"
        label-position="top"
        class="demo-ruleForm"
      >
        <el-form-item :label="$t('Input.FullName')" prop="FullName">
          <!-- <label>{{$t('Input.FullName')}}<span class="tw-text-red-400">*</span></label> -->
          <el-input
            v-model="modelForm.FullName"
            :placeholder="$t('Input.FullNameInput')"
          />
        </el-form-item>
        <el-form-item :label="$t('Input.PhoneNumber')" prop="PhoneNumber">
          <!-- <label>{{$t('Input.PhoneNumber')}}<span class="tw-text-red-400">*</span></label> -->
          <el-input
            v-model="modelForm.PhoneNumber"
            :placeholder="$t('Input.PhoneNumberInput')"
          />
        </el-form-item>
        <el-form-item :label="$t('Input.Email')" prop="Email">
          <!-- <label>{{$t('Input.Email')}}</label> -->
          <el-input
            v-model="modelForm.Email"
            :placeholder="$t('Input.EmailInput')"
          />
        </el-form-item>
      </el-form>
    </template>
    <template #footer>
      <el-button @click="sendReview" class="send-btn">{{
        $t("Button.Send")
      }}</el-button>
    </template>
  </el-dialog>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";
export default {
  props: {
    show: {
      type: Boolean,
      default: false,
    },
    title:{
      type:String,
      default:""
    },
    type:{
      type:Number,
      default:0
    }
  },
  data() {
    // var validateEmail = (rule, value, callback) => {
    //   const regex=/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    //     if (value !== ''&& !value.match(regex)) {
    //       callback(new Error('Bạn đã nhập sai định dạng Email'));
    //     }
    //   };
    return {
      modelDialog: this.show,
      modelForm: {
        FullName: "",
        PhoneNumber: "",
        Email: "",
      },
      rules: {
        FullName: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
          { min: 3, message: "Tên tối thiểu 3 kí tự", trigger: "blur" },
        ],
        PhoneNumber: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        Email: [
          // {validator:validateEmail,trigger: 'blur'}
        ],
      },
    };
  },
  computed: {
    ...mapState("evaluation", ["valueRating", "contentRating"]),
    ...mapState("product", ["product"]),
    ...mapState("order", [
      "isCheckCustomerPurchasedProduct",
      "customerIDPurchased",
    ]),
  },
  created(){
    const me=this
    const customerInfo=JSON.parse(sessionStorage.getItem("Customer"))
          if(customerInfo){
            me.modelForm=JSON.parse(sessionStorage.getItem("Customer"))
          }
  },
  methods: {
    ...mapActions("order", ["checkCustomerPurchasedProduct"]),
    ...mapActions("evaluation", ["insertReviewOfProuct"]),
    setModel() {
      const me = this;
      me.modelDialog = true;
    },
    /**
     * Gửi đánh giá
     */
    async sendReview() {
      const me = this;
      me.$refs["ruleForm"].validate(async (valid) => {
        if (valid) {
          if(me.type===me.$enumeration.CustomerInfoPopup.Review){

            me.$emit('saveReview',me.modelForm)
          }else{
            me.$emit('saveComment',me.modelForm)
          }
          const customerInfo=JSON.parse(sessionStorage.getItem("Customer"))
          if(!customerInfo){
            sessionStorage.setItem("Customer",JSON.stringify(me.modelForm))
          }

          me.modelDialog = false;
        } else {
          return false;
        }
      });
    },
  },
};
</script>

<style scoped>
.send-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<style lang="scss" scoped>
::v-deep {
  .el-dialog__body {
    padding-top: 14px;
  }
}
</style>