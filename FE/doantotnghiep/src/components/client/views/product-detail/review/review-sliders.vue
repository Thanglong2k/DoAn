<template>
  <div class="review-slider-container">
    <div class="item tw-flex tw-items-center"
      v-for="(item,index) in ratingDetail"
      :key="index"
    >
      <label>{{5-index}}</label>
      <el-rate class="rate" :max="1" disabled></el-rate>
      <div class="measure tw-bg-gray-100 tw-h-2 tw-w-52">
        <div class="measure-value tw-bg-green-500 tw-h-full"
          :style="[ratingTotal>0?{'width':`${(item.Quantity/ratingTotal)*100}%`}:{'width':'0px'}]"
        ></div>
      </div>
      <div class="review-number tw-ml-2">{{item.Quantity}}</div>
    </div>
    
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  props: {
    product: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      colors: ["#f7ba2a"],
    };
  },
  computed: {
    ...mapState("evaluation", ["productRatingDetail"]),
    ratingDetail() {
      const me = this;
      let rating = [];
      for (let i = 1; i <= 5; i++) {
        const value = me.productRatingDetail?.filter((item) => {
          return item.Rate === i;
        });
        
        if (value.length===0) {
          rating.push({ Rate: i, Quantity: 0 });
        }else{
          rating.push({ Rate: value[0].Rate, Quantity: value[0].Quantity });
        }
      }
      return rating.reverse();
    },
    ratingTotal(){
      const me=this
      console.log(me.ratingDetail.reduce((prev,current)=>{
        return prev+current.Quantity
      },0));
      return me.ratingDetail.reduce((prev,current)=>{
        return prev+current.Quantity
      },0)
      
    }
  },
  async mounted() {
    const me = this;
    await me.getProductRatingDetail({productID:me.product.ProductAttributeID});
  },
  methods: {
    ...mapActions("evaluation", ["getProductRatingDetail"]),
  },
};
</script>

<style lang="scss" scoped>
::v-deep {
  .el-rate__icon {
    color: #f7ba2a !important;
  }
}
.review-slider-container {
  .item {
    .measure {
      border-radius: 4px;
      overflow: hidden;
    }
  }
}
</style>