<template>
  <div class="product-slider tw-bg-white">
  <v-slide-group
    v-model="model"
    class="tw-p-4"
    active-class="success"
    show-arrows
  >
    <v-slide-item
      v-for="item in products" :key="item.ProductID">
      <item-card class="item-product" :dataItem="item"/>
    </v-slide-item>
  </v-slide-group>
  </div>
</template>

<script>
//import Carousel from "vue-owl-carousel";
import ItemCard from "../item-card";

export default {
  components: {
    ItemCard,
    //Carousel
  },
  props: {
    products: {
      type: Array,
      default: () => {},
    },
  },
  data() {
    return {
      model: null,
    };
  },

};
</script>

<style lang="scss" scoped>
// ::v-deep {
//   .owl-carousel,.owl-stage-outer,.owl-stage,.owl-item {
//     height: 100%;
//   }
//   .owl-carousel{
//       position: relative;
//     .owl-nav{
//         position: absolute;
//         top:calc(50% - 15px);

//     }
//   }
// }
.product-slider{
    border-radius: 4px;
    
}
.v-slide-group{
    padding: 0 45px !important;
    
}
::v-deep{
    .v-slide-group__prev,.v-slide-group__next{
        height:40px;
        width:40px;
        position: absolute;
        top:50%;
    }
    .v-slide-group__prev{
        left:0;
    }
    .v-slide-group__next{
        right:0;
    }
    .v-slide-group__wrapper{
      padding:16px 0;
    }
}
</style>
<style scoped>
.item-product{
        @apply tw-mx-1 !important;
    }
</style>