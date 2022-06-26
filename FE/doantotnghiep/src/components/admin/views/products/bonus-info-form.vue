<template>
  <div class="bonus-info">
    <el-form
      :rules="rules"
      ref="ruleForm"
      label-width="130px"
      label-position="left"
      class="demo-ruleForm"
      v-model="modelForm"
    >
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
      <el-form-item
        :label="$t('Product.Description')"
      >
        <VueEditor v-model="modelForm.Description" @text-change="changeData" />
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { VueEditor } from "vue2-editor";
export default {
  props: {
    productAttribute: {
      type: Object,
      default: () => {},
    },
  },
  components: {
    VueEditor,
  },
  data() {
    return {
      modelForm: {
        Memory: null,
        Price: null,
        PromotionPrice: null,
        StartTimeDate: null,
        EndTimeDate: null,
        Description: null,
      },
      rules: {
       
      },
      memory: null,
      price: null,
      pricePromotion: null,
      startDate: null,
      endDate: null,
      description: null,
    };
  },
  computed: {
    // modelForm:{
    //   get(){
    //     return this.productAttribute
    //   },
    //   set(val){
    //     this.$emit("updateModel", val);
    //   }
    // }
  },
  created() {
    const me = this;
    me.initDataValue();
  },
  methods: {
    changeData() {
      const me = this;
      // const value=JSON.parse(JSON.stringify(me.productAttribute))

      //   value.Memory=me.memory,
      //   value.Price=me.price,
      //   value.PromotionPrice=me.pricePromotion,
      //   value.StartTimeDate=me.startDate,
      //   value.EndTimeDate=me.endDate,
      //   value.Description=me.description

      this.$emit("updateModel", me.modelForm);
    },
    initDataValue() {
      const me = this;
      debugger
      me.modelForm.Memory = me.productAttribute?.Memory;
      if (me.productAttribute.Price) {
        me.modelForm.Price = me.productAttribute.Price;
      } else {
        me.modelForm.Price = null;
      }
      if (me.productAttribute.PromotionPrice) {
        me.modelForm.PromotionPrice = me.productAttribute.PromotionPrice;
      } else {
        me.modelForm.PromotionPrice = null;
      }
      if (me.productAttribute.StartTimeDate) {
        me.modelForm.StartTimeDate = me.productAttribute.StartTimeDate;
      } else {
        me.modelForm.StartTimeDate = null;
      }
      if (me.productAttribute.EndTimeDate) {
        me.modelForm.EndTimeDate = me.productAttribute.EndTimeDate;
      } else {
        me.modelForm.EndTimeDate = null;
      }
      if (me.productAttribute.Description) {
        me.modelForm.Description = me.productAttribute.Description;
      } else {
        me.modelForm.Description = null;
      }
    },
  },
};
</script>

<style lang="scss" scoped>
::v-deep {
  .ql-container {
    height: auto !important;
  }
}
.bonus-info{
  height:400px;
  overflow: auto;
}
</style>