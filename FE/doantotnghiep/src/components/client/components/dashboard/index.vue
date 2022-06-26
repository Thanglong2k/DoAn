<template>
  <div class="dashboard">
    <img class="tag-sale-left" :src="imageSaleLeft" />
    <img class="tag-sale-right" :src="imageSaleRight" />
    <div class="dashboard-main">
      <div class="banner tw-flex">
        <v-carousel class="carousel" hide-delimiter-background :cycle="true" interval="3000" >
          <v-carousel-item
            v-for="(item, index) in imageCarousels"
            :key="index"
            :src="item"
          >
          </v-carousel-item>
        </v-carousel>
        <div class="banner-advertise tw-flex-1 tw-h-full tw-ml-4 tw-cursor-pointer">
          <div class="advertise-top tw-h-1/2">
            <img class="tw-h-full" :src="imageBannerTop"/>
          </div>
          <div class="advertise-bottom tw-h-1/2 tw-mt-2">
            <img class="tw-h-full" :src="imageBannerBottom"/>
          </div>
        </div>
      </div>
      <area-card class="tw-mt-4" title="Sản phẩm bán chạy nhất" :showReviewAll="true" background-color="tw-bg-yellow-400" text-color="tw-text-red-600" :grid="true" :columnNumber="5">
        <item-card v-for="item in bestSellingProducts" :key="item.ProductID" :dataItem="item"/>
      </area-card>
      <div class="banner tw-h-40 tw-w-full tw-grid tw-grid-cols-2 tw-gap-2 tw-mt-4">
        <img class="tw-h-full tw-w-full tw-mr-2" :src="imageBannerLeft" >
        <img class="tw-h-full tw-w-full " :src="imageBannerRight" >
      </div>
      <area-card class="tw-mt-4" title="Sản phẩm mới" :showReviewAll="true" :grid="true" :columnNumber="5">
        <item-card v-for="item in newProducts" :key="item.ProductID" :dataItem="item" />
        <!-- <template v-slot:more>
          <el-button>Xem thêm</el-button>
        </template> -->
      </area-card>
    </div>
  </div>
</template>

<script>
import AreaCard from "@/components/controls/area-card";
import ItemCard from "@/components/controls/item-card";
import {mapState,mapActions, mapMutations} from 'vuex'
export default {
  components: {
    AreaCard,
    ItemCard,
  },
  data() {
    return {
      imageSaleLeft: require("@/assets/images/clients/backgrounds/sale-1.png"),
      imageSaleRight: require("@/assets/images/clients/backgrounds/sale-2.png"),
      imageCarousels: [
        require("@/assets/images/clients/carousels/slider-1.png"),
        require("@/assets/images/clients/carousels/slider-2.png"),
        require("@/assets/images/clients/carousels/slider-3.png"),
        require("@/assets/images/clients/carousels/slider-4.png"),
      ],
      imageBannerTop: require("@/assets/images/clients/banner_adv/flip_right.webp"),
      imageBannerBottom: require("@/assets/images/clients/banner_adv/13-desk.webp"),
      imageBannerLeft: require("@/assets/images/clients/banner_adv/banner-left.png"),
      imageBannerRight: require("@/assets/images/clients/banner_adv/banner-right.png"),
      newProductNumber:15,//số sản phẩm mới
      bestSellingProductNumber:10,//số sản phẩm bán chạy nhất
      stage:60//khoảng thời gian lấy sản phẩm bán chạy nhất
    };
  },
  async mounted(){
    const me=this
    me.$commonFn.showMark("main")
    await me.getNewProducts({productNumber:me.newProductNumber})
    await me.getBestSellingProducts({productNumber:me.bestSellingProductNumber,stage:me.stage})
    me.$commonFn.hideMark("main")

  },
  computed:{
    ...mapState('product',['products','newProducts','bestSellingProducts'])
  },
  methods:{
    ...mapActions('product',['getProducts','getNewProducts','getBestSellingProducts'])
  }
};
</script>

<style lang="scss" scoped>

.dashboard {
  .tag-sale {
    &-left {
      position: fixed;
      top: 28%;
      left: 3%;
    }
    &-right {
      position: fixed;
      top: 28%;
      right: 3%;
    }
  }
  .dashboard-main {
    .banner {
      height: 40% !important;
      .carousel {
        height: 100% !important;
        width: 70% !important;
      }
    }
  }
}
</style>
<style>
.carousel .v-image {
  height: 100% !important;
}
</style>>
