<template>
  <v-sheet class="mx-auto" elevation="8">
    <v-slide-group v-model="model" class="pa-4"  show-arrows>
      <v-slide-item v-for="n in 15" :key="n">
        <div
          class="
            product
            tw-relative
            tw-w-52
            tw-p-4
            tw-bg-pink-50
            tw-cursor-pointer
            tw-overflow-hidden
          "
        >
          <img :src="imgProduct" class="product-image tw-w-full" />
          <div class="product-name tw-mt-3">Iphone 6s-Plus</div>
          <div class="product-price tw-mt-1"
            :class="{'tw-line-through':discount,'tw-font-bold':!discount}"
          >18.000.000</div>
          <div class="product-price tw-font-bold tw-mt-1">{{newPrice}}</div>
          <div class="tag-discount tw-bg-red-400 tw-absolute tw-top-2 tw-left-2 tw-text-white tw-font-bold">-{{discount}}%</div>
          <div class="product-rating tw-mt-1">3 sao</div>
          <div
            class="
              event
              tw-absolute tw-top-0 tw-left-0 tw-h-full tw-w-full tw-flex
            "
          >
            <div
              class="overlay tw-absolute tw-top-0 tw-left-0 tw-h-full tw-w-full"
            ></div>
            <v-btn class="btn-detail tw-mb-3" @click="seeProductDetail"
              >{{$t('Input.Ca')}}</v-btn
            >
            <v-btn class="btn-add-to-card" @click="addToCard"
              >{{$commonFn.formatMoney('13333')}}</v-btn
            >
          </div>
        </div>
      </v-slide-item>
    </v-slide-group>
  </v-sheet>
</template>

<script>
import Common from '../../../scripts/common'
export default {
  data() {
    return {
      imgProduct: require("@/assets/images/avatars/1.jpg"),
      discount:10
    };
  },
  computed:{
    newPrice(){
      const money=18000000*(100-10)/100
      return Common.formatMoney(money.toString()) 
    }
  },
  created(){
  },
  methods: {
    /**
     * Xem chi tiết sản phẩm
     */
    seeProductDetail() {},
    /**
     * Thêm sản phẩm vào giỏ hàng
     */
    addToCard() {},
  },
};
</script>

<style lang="scss" scoped>
.product {
  height: 300px;
  border-radius: 10px;

  margin-left: 50px;
  &:hover {
    box-shadow: 0 0 10px rgb(87, 164, 236);
    .product-image {
      transform: scale(1.15);
    }
    .event {
      visibility: visible;
    }
  }
  .product-image {
    height: 60%;
  }
  .event {
    align-items: center;
    flex-direction: column;
    justify-content: center;
    visibility: hidden;

    .overlay {
      background-color: rgb(3, 3, 3);
      opacity: 0.5;
    }
    .btn-detail {
      background-color: #fff;
      width: 70%;
    }
    .btn-add-to-card {
      background-color: rgb(255, 94, 0);
      color: #fff;
      width: 70%;
    }
  }
  .tag-discount{
    padding:4px 8px;
    border-radius: 40%;
  }
}
</style>