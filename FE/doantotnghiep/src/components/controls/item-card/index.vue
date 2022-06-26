<template>
  <v-card
    class="tw-border tw-relative"
    :max-width="maxWidth"
    :max-height="maxHeight"
    :min-width="minWidth"
    :min-height="minHeight"
    :height="height"
    :width="width"
    @mouseenter="hover"
    @mouseleave="leave"
  >
    <template slot="progress">
      <v-progress-linear
        color="deep-purple"
        height="10"
        indeterminate
      ></v-progress-linear>
    </template>

    <div
      class="image tw-flex tw-justify-center tw-p-2 tw-cursor-pointer"
      :style="[hoverCard ? { transform: 'scale(1.05)' } : {}]"
    >
      <img :src="dataItem.Avatar" @click="buyNow" />
    </div>

    <v-card-title class="name">{{ dataItem?dataItem.ProductName + " " + productAttributeSelected.Memory + "GB":""}}</v-card-title>

    <v-card-text>
      <div class="memory tw-flex tw-bg-gray-200" v-if="dataItem && dataItem.ProductAttributes.length>1">
        <div
          class="
            memory-item
            hover:tw-bg-black hover:tw-text-white
            tw-cursor-pointer tw-flex tw-justify-center tw-py-1
          "
          :class="[{'tw-bg-black tw-text-white':productAttribute.ProductAttributeID===productAttributeSelected.ProductAttributeID}]"
          :style="[{ width: 100 / dataItem.ProductAttributes.length + '%' }]"
          v-for="(productAttribute, index) in dataItem.ProductAttributes"
          :key="productAttribute.ProductAttributeID"
          @click="handleSelectMemory(index)"
        >
          {{ productAttribute.Memory }}
        </div>
      </div>
      <div
        :class="[
          {
            'tw-text-gray-400 tw-line-through tw-text-xs':
              promotionPrice!==null&&promotionPrice!==undefined&&promotionPrice!==0 && notExpire,
          },
          {
            'tw-text-black tw-font-bold tw-text-base tw-text-red-600':
              promotionPrice===null||promotionPrice===undefined||promotionPrice===0 || !notExpire,
          },
        ]"
      >
        {{ price?$commonFn.formatMoney2(productAttributeSelected.Price.toString()):0 }} VNĐ
      </div>
      <div
        class="tw-text-black tw-font-bold tw-text-base tw-text-red-600"
        v-if="promotionPrice!==null&&promotionPrice!==undefined&&promotionPrice!==0 && notExpire"
      >
        {{ $commonFn.formatMoney(promotionPrice.toString()) }} VNĐ
      </div>

      <!-- <div
        v-if="dataItem.Quantity === 0"
        class="
          out-of-stock
          tw-mt-2
          tw-bg-red-200
          tw-border-red-500
          tw-border-solid
          tw-text-red-500
          tw-px-3
        "
      >
        {{ $t("DetailProduct.OutOfStock") }}
      </div> -->
      <div class="mx-0 tw-flex tw-mt-2">
        <v-rating
          :value="rate"
          background-color="orange"
          color="orange"
          half-increments
          :length="5"
          size="16"
          readonly
          dense
        ></v-rating>

        <div
          class="tw-text-gray-400 tw-ml-4 tw-flex tw-items-center tw-text-xs"
        >
          {{ rate }} ({{ countRating }})
        </div>
      </div>
    </v-card-text>
    <div
      class="action tw-px-4 tw-pb-4 tw-flex"
      :style="[
        hoverCard ? { visibility: 'visible' } : { visibility: 'hidden' },
      ]"
    >
      <!-- :style="[hoverCard?{'display':'flex'}:{'display':'none'}]" -->

      <el-button
        size="small"
        class="btn-buy tw-mr-2 tw-p-2 tw-bg-red-600"
        @click="buyNow"
        >{{ $t("Button.BuyNow") }}</el-button
      >
      <el-button size="small" class="btn-compare" @click="compareProduct" v-if="dataItem.Type===$enumeration.ProductType.Phone">{{
        $t("Button.Compare")
      }}</el-button>
    </div>
    <div class="tag-area tw-absolute
      tw-top-0.5
      tw-left-0.5">
      <tag-sale v-if="newProduct" :newProduct="true" />
      <tag-sale class="mt-1" v-if="promotionPrice && notExpire" :discount="discount" />
      </div>
  </v-card>
</template>

<script>
import TagSale from "../tag-sale";
import { mapState, mapMutations } from "vuex";
export default {
  components: {
     TagSale,
  },
  props: {
    maxWidth: {
      type: String,
    },
    maxHeight: {
      type: String,
    },
    minWidth: {
      type: String,
      default: "0",
    },
    minHeight: {
      type: String,
      default: "0",
    },
    width: {
      type: String,
    },
    height: {
      type: String,
    },
    dataItem: {
      type: Object,
      default: () => {},
    },

    // dataItem: {
    //   type: Object,
    //   default: () => {},
    // },
  },
  data() {
    return {
      productAttributeSelected:this.dataItem?.ProductAttributes[0],
      // dataItem: {
      //   Name: "Long",
      //   Image:
      //     "https://images.fpt.shop/unsafe/fit-in/240x240/filters:quality(90):fill(white)/fptshop.com.vn/Uploads/Originals/2021/9/15/637673213598401263_iphone-13-pro-max-dd-1.jpg",
      //   Price: 1000000,
      //   Promotion: 10,
      //   Rating: 3.5,
      //   CountRating: 144,

      // },
      hoverCard: false,
    };
  },

  computed: {
    newProduct(){

      return Math.round((new Date()-new Date(this.dataItem.CreatedDate))/(1000*60*60*24))<=30;
    },
    discount() {
      return (
        100 -
        Math.round((this.productAttributeSelected?.PromotionPrice / this.productAttributeSelected?.Price) * 100)
      );
    },
    promotionPrice(){
      return this.productAttributeSelected?.PromotionPrice
    },
    notExpire(){
      return new Date(this.productAttributeSelected.PromotionEnd)>=new Date()
    },
    price(){
      return this.productAttributeSelected?.Price
    },
    rate(){
      return this.productAttributeSelected?.Rate?this.productAttributeSelected.Rate:0
    },
    countRating(){
      return this.productAttributeSelected?.CountRating?this.productAttributeSelected.CountRating:0
    }
  },

  methods: {
    ...mapMutations("product", ["setCompareProducts"]),
    buyNow() {
      const me = this;
      localStorage.setItem("Id_Product", me.dataItem.ProductID);
      sessionStorage.setItem("ProductAttribute", JSON.stringify(me.productAttributeSelected));
      if (me.$route.name !== "ProductDetail") {
        me.$router.push({
          name: "ProductDetail",
          params: { productMeta: me.dataItem.Meta },
        });
      } else {
        me.$router.replace({
          name: "ProductDetail",
          params: { productMeta: me.dataItem.Meta },
        });
        me.$router.go();
      }
    },
    compareProduct() {
      const me = this;
      const compareProduct = [];
      const productFirst= JSON.parse(JSON.stringify(me.dataItem))
      productFirst.ProductAttributes=[]
      productFirst.ProductAttributes.push(me.productAttributeSelected)
      compareProduct[0]=productFirst;
      me.setCompareProducts(compareProduct)
      sessionStorage.setItem("CompareProduct", JSON.stringify(compareProduct));
      me.$router.push("/compare-product");
    },
    hover() {
      this.hoverCard = true;
    },
    leave() {
      this.hoverCard = false;
    },
    handleSelectMemory(index){
      const me=this
      me.productAttributeSelected=me.dataItem.ProductAttributes[index]
    }
  },
};
</script>

<style scoped>
.image {
  height: 200px;
  border-radius: 10px !important;
  overflow: hidden !important;
}
img {
  width: 100%;
  border-radius: 10px !important;
}
.v-card {
  border-radius: 10px !important;
  overflow: hidden !important;
  margin: 0 !important;
}
.v-card:hover {
  box-shadow: 0 0 5px rgb(214, 214, 214) !important;
}

.v-card__title {
  @apply tw-pt-0 tw-pb-2 tw-px-2 tw-text-lg tw-text-gray-700 tw-font-semibold !important;
}
.v-card__text {
  @apply tw-p-2 tw-pt-0 !important;
}
.btn-buy {
  @apply tw-bg-red-600 tw-text-red-50 tw-text-xs !important;
  border-radius: 8px;
}
.btn-compare {
  @apply tw-text-xs !important;
  border-radius: 8px;
}
.out-of-stock {
  border-width: 1px;
  border-radius: 10px;
  width: fit-content;
}
.memory .memory-item {
  border-radius: 4px;
}
</style>
