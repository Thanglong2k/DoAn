<template>
  <div class="product-carousel tw-w-full tw-flex tw-flex-col tw-items-center">
    <div class="image-container">
      <!-- <Magnifier
        class="tw-w-full tw-h-full tw-object-cover"
        :src="imageProduct[indexImage]"
        :src-large="imageProduct[indexImage]"
      >
      </Magnifier> -->
<ZoomImage v-if="imageProduct" :img-normal="imageProduct[indexImage]" :scale="2"/>
      <!-- <img class="tw-w-full tw-h-full tw-object-cover" :src="image[indexImage]"/> -->
    </div>
    <div class="image-slide tw-h-20 tw-relative tw-flex tw-justify-center mt-4">
      <div
        class="prev tw-h-full tw-flex tw-items-center tw-mr-4 tw-cursor-pointer"
        @click="prevImage"
      >
        {{prev}}
      </div>
      <div class="image tw-flex tw-h-full tw-overflow-hidden">
        <img
          class="tw-h-full tw-w-20 tw-object-cover tw-mr-4"
          :class="{ 'border-focus': index === indexImage }"
          :src="item"
          v-for="(item, index) in imageProduct"
          :key="index"
          @click="selectImage(index)"
          v-show="showImage(index)"
        />
      </div>

      <div
        class="next tw-h-full tw-flex tw-items-center tw-cursor-pointer"
        @click="nextImage"
      >
        >
      </div>
    </div>
  </div>
</template>

<script>
import Magnifier from "@/components/controls/magnifier";
//import InnerImageZoom from 'vue-inner-image-zoom';
import ZoomImage from "@/components/controls/zoom-image";
export default {
  components: {
    
    ZoomImage
  },
  props: {
    imageProduct: {
      type: Array,
      default: () => {},
    },
  },
  data() {
    return {
      image: [
        "https://didongviet.vn/pub/media/catalog/product//i/p/iphone-13-blue-didongviet.jpg",
        "https://didongviet.vn/pub/media/catalog/product//i/p/iphone-13-blue-didongviet.jpg",
        "https://didongviet.vn/pub/media/catalog/product//i/p/iphone-13-pink-didongviet.jpg",
        "https://didongviet.vn/pub/media/catalog/product//i/p/iphone-13-blue-didongviet.jpg",
      ],
      indexImage: 0,
      prev:"<"
    };
  },
  computed: {},
  methods: {
    prevImage() {
      const me = this;
      me.indexImage--;
      if (me.indexImage < 0) {
        me.indexImage = me.imageProduct.length - 1;
      }
    },
    nextImage() {
      const me = this;
      me.indexImage++;
      if (me.indexImage > me.imageProduct.length - 1) {
        me.indexImage = 0;
      }
    },
    selectImage(index) {
      const me = this;
      me.indexImage = index;
    },
    showImage(index) {
      const me = this;
      console.log(index);
      //nếu chọn ảnh đầu tiên thì hiển thị các ảnh từ vị trí 0-3
      //nếu chọn ảnh cuối thì hiển thị các ảnh từ vị trí cuối đến cuối -3
      if (
        (me.indexImage === 0 && index < 3) ||
        (me.indexImage === me.imageProduct.length - 1 &&
          index > me.imageProduct.length - 1 - 3) ||
        (me.indexImage !== 0 &&
          me.indexImage !== me.imageProduct.length - 1 &&
          index >= me.indexImage - 1 &&
          index <= me.indexImage + 1)
      ) {
        // || (me.indexImage!==0 && me.indexImage!==me.image.length && (index))
        return true;
      } else {
        return false;
      }
    },
  },
};
</script>
<style scoped>
@import '../../../../node_modules/vue-inner-image-zoom/lib/vue-inner-image-zoom.css';
</style>
<style lang="scss" scoped>

.product-carousel {
  height: 600px;

  .image-container {
    z-index: 2;
    width: 100%;
    height: calc(100% - 81px - 17px);
    padding:24px;
  }
  .image-slide {
    width: 500px;
  }
}
.border-focus {
  border: 1px solid red;
}
</style>