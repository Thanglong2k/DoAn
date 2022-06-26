<template>
  <div class="detail-product-container" v-if="product">
    <div class="header-area tw-flex tw-justify-between tw-items-center tw-bg-white tw-p-6">
      <div class="header-area-left tw-text-2xl tw-font-semibold">{{ header }}</div>
      <div class="header-area-right tw-flex tw-items-center">
        <el-rate :value="rating" disabled :allow-half="true"></el-rate>
        <div class="number-rating tw-text-xs">
          {{ countRating }} đánh giá
        </div>
      </div>
    </div>
    <!-- Thông tin sản phẩm -->
    <div class="product-info tw-flex tw-bg-white">
      <div class="product-info-left tw-w-3/5">

        <div class="product-images tw-w-full">

<ProductCarousel :imageProduct="imageProduct"/>
        </div>
        <!-- Thông số kỹ thuật -->
        <div class="basic-specifications tw-w-full tw-bg-gray-200 tw-p-4" v-if="product.Type===$enumeration.ProductType.Phone">
          <div class="screen tw-flex tw-items-center">
            <span class="tw-pr-2 tw-flex tw-items-center"
              ><img :src="iconMonitor"
            /></span>
            <span>{{ product.Monitor }}"</span>
          </div>
          <div class="ram tw-flex">
            <span class="tw-pr-2 tw-flex tw-items-center"
              ><img :src="iconRam"
            /></span>
            <span>{{ product.Ram }} GB</span>
          </div>
          <div class="internal-memory tw-flex">
            <span class="tw-pr-2 tw-flex tw-items-center"
              ><img :src="iconCamera"
            /></span>
            <span>{{ product.Camera }}</span>
          </div>
          <div class="pin tw-flex">
            <span class="tw-pr-2 tw-flex tw-items-center"
              ><img :src="iconBattery"
            /></span>
            <span>{{ product.Battery }} mAh</span>
          </div>
          <div class="details tw-text-blue-600 tw-cursor-pointer" @click="detailSpecifications">
            {{ $t("DetailProduct.DetailSpecification") }}
          </div>
        </div>
        
      </div>
      <div class="product-info-right tw-flex-1 tw-px-4">
        <div class="product-price tw-flex tw-items-end">
          <div class="promotion-price tw-font-bold tw-text-red-500 tw-mr-5 tw-text-2xl"
            v-if="productAttributeSelected&&productAttributeSelected.PromotionPrice!==0"
          >{{ productPromotionPrice }}</div>
          <div :class="[
          {'price tw-font-bold tw-text-red-500 tw-mr-5 tw-text-2xl':productAttributeSelected.PromotionPrice===0},
          {'price tw-line-through tw-text-xs tw-text-gray-500':productAttributeSelected.PromotionPrice!==0}]">{{ productPrice }}</div>
        </div>
        <div class="memory tw-flex tw-bg-gray-200" v-if="product.ProductAttributes.length>1">
          <div
          class="
            memory-item
            hover:tw-bg-black hover:tw-text-white
            tw-cursor-pointer tw-flex tw-flex-col tw-items-center tw-py-1
          "
          :class="[{'tw-bg-gray-300':productAttribute.ProductAttributeID===productAttributeSelected.ProductAttributeID}]"
          :style="[{ width: 100 / product.ProductAttributes.length + '%' }]"
          v-for="(productAttribute, index) in product.ProductAttributes"
          :key="productAttribute.ProductAttributeID"
          @click="handleSelectMemory(index)"
        >
          <div class="memory-value">{{ productAttribute.Memory }}</div>
          <div class="price">{{$commonFn.formatMoney(productAttribute.Price.toString())}}</div>
        </div>
        </div>
        <el-button class="btn-add-cart tw-w-full" @click="addToCart">{{
          $t("Button.BuyNow")
        }}</el-button>
        <div class="specifications tw-w-full">
          <v-card-title class="specification-title">Thông số kỹ thuật</v-card-title>
          <summary-table :product="product" :productSelected="productAttributeSelected"/>
          <div class="tw-mt-4 tw-text-center tw-cursor-pointer tw-text-blue-600" @click="detailSpecifications">Xem chi tiết ></div>
        </div>
        <Post v-if="posts.length>0" :product="product" :posts="posts"/>
      </div>
    </div>
    <!-- Mô tả sản phẩm -->
        <div class="product-description tw-bg-white tw-p-6 tw-overflow-hidden tw-relative" :style="[more?{}:{'height':'300px'}]" v-if="productAttributeSelected.Description" >
          <div class="description-title tw-text-xl tw-text-gray-600 tw-font-semibold tw-mb-4">Đặc điểm nổi bật</div>
          <div class="description-content" v-html="productAttributeSelected.Description"></div>
          <div class="more tw-flex tw-justify-center tw-absolute tw-bottom-4 tw-w-full tw-z-10" v-if="!more">
            <el-button @click="onMore">Xem thêm</el-button>
          </div>
          <div class="button-overlay tw-absolute" v-if="!more"></div>
        </div>
    <!-- Sản phẩm liên quan -->
    <div class="related-product tw-bg-white tw-mt-4" v-if="relatedProducts.length>0">
      <div class="header-area tw-h-16">
        <v-card-title> {{ $t("DetailProduct.RelatedProduct") }} </v-card-title>
      </div>
      <div class="content-area">
        <product-slider :products="relatedProducts" />
      </div>
    </div>
    <!-- Đánh giá sản phẩm -->

    <Review :product="productAttributeSelected"/>
    <Comment :product="productAttributeSelected"/>
    <DetailSpecificationsPopup ref="popup" title="Thông số kỹ thuật" :product="product" :productSelected="productAttributeSelected" :manufacturer="manufacturer"/>
  </div>
</template>

<script>
import ProductCarousel from "@/components/controls/product-carousel";
import Review from "@/components/client/views/product-detail/review/review.vue";
import Comment from "@/components/client/views/product-detail/comment/comment.vue";
import ProductSlider from "@/components/controls/product-slider";
import SummaryTable from "@/components/client/views/product-detail/specifications/summary-table.vue";
import DetailSpecificationsPopup from "@/components/client/views/product-detail/popup/detail-specifications.vue";
import Post from "@/components/client/components/post/recommend-post-list.vue";


import { mapState, mapActions, mapMutations } from "vuex";
export default {
  components: {
    ProductCarousel,
    Review,
    Comment,
    ProductSlider,
    SummaryTable,
    DetailSpecificationsPopup,
    Post
  },
  data() {
 
       return {
      ratingProduct: 4.5,
      rating2: 0,
      iconMonitor: require("@/assets/icons/product-detail/smartphone.png"),
      iconRam: require("@/assets/icons/product-detail/cpu.png"),
      iconBattery: require("@/assets/icons/product-detail/battery.png"),
      iconCamera: require("@/assets/icons/product-detail/camera.png"),
      productAttributeSelected:null,
      imageProducts: [
        require("@/assets/images/clients/carousels/slider-1.png"),
        require("@/assets/images/clients/carousels/slider-2.png"),
        require("@/assets/images/clients/carousels/slider-3.png"),
        require("@/assets/images/clients/carousels/slider-4.png"),
      ],
      relatedProductNumber: 10,


      textarea: "",
      more:false
    };
  },
  computed: {
    ...mapState("product", ["relatedProducts", "product"]),
    ...mapState("post", ["posts"]),
    ...mapState('manufacturer',['manufacturer']),
  imageProduct(){
    const images=this.product?.Image.split(',')
    images?.unshift(this.product?.Avatar)
    return images
  },
  rating(){
    return this.productAttributeSelected?.Rate?this.productAttributeSelected?.Rate:0
  },
  countRating(){
    return this.productAttributeSelected?.CountRating?this.productAttributeSelected?.CountRating:0
  },
    /**
     * header tên sản phẩm
     */
    header() {
      return "Điện thoại " + this.product?.ProductName + ' ' + this.productAttributeSelected?.Memory + 'GB';
    },
    /**
     * giá sản phẩm
     */
    productPrice() {
      console.log(this.productAttributeSelected);
      return this.productAttributeSelected?.Price?this.$commonFn.formatMoney(this.productAttributeSelected?.Price.toString()):0;
    },
    /**
     * Giá giảm
     */
    productPromotionPrice(){
      return this.productAttributeSelected?.PromotionPrice?this.$commonFn.formatMoney(this.productAttributeSelected?.PromotionPrice.toString()):0;
    },
    /**
     * Ảnh sản phẩm
     */
    productImage() {
      return {
        thumbs: [
          {
            id: 1,
            url: this.product.Image,
          },
        ],
        normal_size: [
          {
            id: 1,
            url: this.product.Image,
          },
        ],
        large_size: [
          {
            id: 1,
            url: this.product.Image,
          },
        ],
      };
    },
  },
  async mounted() {
    const me = this;
    me.$commonFn.showMark("main")
    const productID = localStorage.getItem("Id_Product");
    me.productAttributeSelected = JSON.parse(sessionStorage.getItem("ProductAttribute"));
    console.log(me.productAttributeSelected);
    await me.getProductByID(productID);
    
    const params = {
      productNumber: me.relatedProductNumber,
      manufacturerID: me.product.ManufacturerID,
      productID: me.product.ProductID,
    };

    await me.getRelatedProducts(params);
    const paramPost={
      productID:me.product.ProductID,
      pageIndex:1,
      pageSize:5
    }
    await me.getPostByProductID(paramPost)
    await me.getManufacturerByID(me.product.ManufacturerID)
    me.$commonFn.hideMark("main")
  },
  methods: {
    ...mapActions("product", ["getRelatedProducts", "getProductByID"]),
    ...mapMutations("cart", ["setProductCartLength", "setProductCart"]),
    ...mapActions("comment", ["getCommentByProductID","insertCommentOfProduct"]),
    ...mapActions("evaluation", ["getEvaluationByProductID","getProductRatingDetail"]),
    ...mapActions("post", ["getPostByProductID"]),
    ...mapActions('manufacturer',['getManufacturerByID']),
    onMore(){
      const me=this
      me.more=true
    },
    addToCart() {
      const me = this;
      let newCart = [];
      if(me.product?.Quantity<=0){
        const h = me.$createElement;
        me.$notify({
                title: me.$t("Notify.Warning"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.ProductOutOfStock")
                ),
                type: "warning",
              });
      }else{
        const cart = JSON.parse(localStorage.getItem("productCart"));
      //nếu trong giỏ không có sản phẩm thì thêm mới vào giỏ
      if (!cart || cart.length===0) {
        const productCart = JSON.parse(JSON.stringify(me.product))
        productCart.ProductAttributes=[]
        me.productAttributeSelected.Quantity=1
        productCart.ProductAttributes.push(me.productAttributeSelected)
        newCart.push(productCart);
      } else {
        // const checkProductExists = cart.some(
        //   (item) => item.ProductAttributeSelected[0].ProductAttributeID === me.productAttributeSelected.ProductAttributeID
        // );
        const indexProduct = cart.findIndex(
          (item) => item.ProductAttributes[0].ProductAttributeID === me.productAttributeSelected.ProductAttributeID
        );
        //nếu trong giỏ tồn tại sản phẩm thêm mới thì chỉ tăng số lượng
        if (indexProduct>=0) {
          
          cart[indexProduct].ProductAttributes[0].Quantity++
          newCart = [...cart];
        } else {
          //không tồn tại sản phẩm thêm mới thì thêm nó vào giỏ trước rồi thêm dánh sách sản phẩm cũ vào sau để sản phẩm mới lên đầu
          newCart=JSON.parse(JSON.stringify(cart));
          const productCart = JSON.parse(JSON.stringify(me.product))
          productCart.ProductAttributes=[]
        productCart.ProductAttributes.push(me.productAttributeSelected)
          productCart.ProductAttributes[0].Quantity = 1;
          newCart.push(productCart);
        }
      }
      localStorage.setItem("productCart", JSON.stringify(newCart));
      localStorage.setItem("productCartLength", newCart.length);
       me.setProductCartLength(newCart.length)
        me.setProductCart(newCart)
      me.$router.push("/product-cart");
      }
      
    },
    handleSelectMemory(index){
      const me=this
      me.productAttributeSelected=me.product.ProductAttributes[index]
      sessionStorage.setItem("ProductAttribute",JSON.stringify(me.productAttributeSelected))
      me.getCommentOfProduct(1);
      me.getEvaluationOfProduct(1);
      me.getProductRatingDetail({productID:me.productAttributeSelected.ProductAttributeID});

    },
    /**
     * Lấy danh sách bình luận của sản phẩm
     */
    getCommentOfProduct(pageIndex) {
      const me = this;
      const params = {
        productAttributeID: me.productAttributeSelected.ProductAttributeID,
        pageindex: pageIndex,
        pageSize: 5,
      };
      
      me.getCommentByProductID(params);
    },
    /**
     * Lấy danh sách đánh giá của sản phẩm
     */
    getEvaluationOfProduct(pageIndex) {
      const me = this;
      const params = {
        productAttributeID: me.productAttributeSelected.ProductAttributeID,
        pageindex: pageIndex,
        pageSize: 5,
      };
      me.getEvaluationByProductID(params);
    },
    detailSpecifications(){
      const me=this
      me.$refs.popup.setModelDialog()
    }

  },
};
</script>

<style scoped>
.btn-add-cart {
  @apply tw-bg-red-500 tw-text-white !important;
}
.specification-title{
  @apply tw-font-semibold tw-ml-0 !important;
}
</style>
<style lang="scss" scoped>
::v-deep {
  .v-image__image {
    background-size: contain !important;
  }
  .zoomer-base-container {
    height: auto !important;
    width: 100% !important;
    .thumb-list {
      width: 100% !important;
    }
  }
}
.detail-product-container {
  .header-area {
    &-left {
    }
    &-right {
      .number-rating {
      }
    }
  }
  .product-info {
    &-left {
      .basic-specifications {
      }
      .product-description {
      }
    }
    &-right {
      .btn-add-cart {
        
      }
      .memory-item{

      }
      .specifications{

      }
    }
  }
  .related-product {
    border-radius: 4px;
  }
  .button-overlay{
    bottom: -2px;
    left: 0;
    right: 0;
    display: block;
    padding: 80px 0 20px;
    text-align: center;
    background-image: linear-gradient(to bottom,rgba(255,255,255,0),#fff 98%,rgba(0,0,0,0));
  }

}
</style>