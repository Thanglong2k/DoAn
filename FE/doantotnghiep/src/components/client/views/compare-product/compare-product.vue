<template>
  <div class="compare-product-contain">
    <!-- <el-input
      prefix-icon="el-icon-search"
      :placeholder="$t('Input.CompareSearch')"
      :clearable="true"
      v-model="keywork"
      @input="filterProduct"
    ></el-input> -->
    <el-autocomplete
      class="inline-input"
      v-model="keywork"
      :fetch-suggestions="querySearch"
      prefix-icon="el-icon-search"
      :placeholder="$t('Input.CompareSearch')"
      :clearable="true"
      :trigger-on-focus="false"
      @select="handleSelect"
    >
      <template slot-scope="{ item }">
      <div class="autocomplete-item tw-flex">
        <div class="product-image tw-w-12"><img class="tw-w-full" :src="item.Product.Avatar"/></div>
        <div class="value">{{ item.Product.ProductName+" "+item.ProductAttribute.Memory+" GB" }}</div>
      </div>
      
  </template>
    </el-autocomplete>
    <div
      class="product-info tw-w-full tw-mt-4"
      v-if="Object.keys(productData).length > 0"
    >
      <div class="product-image tw-w-full tw-h-64">
        <img
          class="tw-h-full tw-w-full tw-object-contain"
          :src="productData.Avatar"
        />
      </div>
      <div class="product-price tw-mt-3 tw-text-center">
        Giá :
        <span class="tw-text-red-400 tw-font-bold">{{ $commonFn.formatMoney(productData.Price.toString()) }}</span>
      </div>
      <div
        class="
          view-detail
          tw-text-center tw-text-xs tw-cursor-pointer tw-text-blue-400
          hover:tw-text-blue-300
        "
        @click="viewProductDetail"
      >
        Xem chi tiết >
      </div>
    </div>
    <div class="recommand-product-area tw-mt-4" v-else>
      <div class="recommand-text tw-text-xs tw-text-gray-400">
        Gợi ý sản phẩm so sánh
      </div>
      <div
        class="
          recommand-product
          tw-h-80 tw-grid tw-grid-cols-2 tw-grid-rows-2 tw-gap-2 tw-mt-2
        "
      >
        <div
          class="
            product-item
            tw-w-full tw-h-full tw-p-4 tw-cursor-pointer
            hover:tw-bg-red-50
          "
          v-for="item in recommoandProducts"
          :key="item.ProductID"
          @click="chooseCompareProduct(item)"
        >
          <div class="product-item-image">
            <img
              class="tw-h-full tw-w-full tw-object-contain"
              :src="item.Image"
            />
          </div>
          <div class="product-item-name tw-pt-2 tw-h-5 tw-text-center">
            {{ item.ProductName }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapMutations, mapActions } from "vuex";
export default {
  props: {
    product: {
      type: Object,
      default: () => {},
    },
    number:{
      type:Number,
      default:1
    }
    
  },
  data() {
    return {
      keywork: "",
      // productData:{},
    };
  },
  computed: {
    ...mapState("product", ["recommoandProducts","compareProducts","products"]),
    productData(){
      const me=this
      const productData=JSON.parse(JSON.stringify(me.product))
      if (productData&&Object.keys(productData).length > 0) {
        me.keywork = productData.ProductName + " " + productData.ProductAttributes[0].Memory+" GB";
      }
      return productData
    }
  },

  methods: {
    ...mapMutations("product",["setCompareProducts"]),
    ...mapActions("product",["getProductAndProductAttribute"]),
    /**
     * Xem chi tiết sản phẩm
     */
    viewProductDetail() {
      const me = this;
      localStorage.setItem("Id_Product", me.productData.ProductID);
      sessionStorage.setItem("ProductAttribute", JSON.stringify(me.productData.ProductAttributes[0]));
      me.$router.push({
        name: "ProductDetail",
        params: { productMeta: me.productData.Meta },
      });
    },
    /**
     * Chọn sản phẩm so sánh
     */
    chooseCompareProduct(product){
      const me=this
      const compareProduct=JSON.parse(sessionStorage.getItem('CompareProduct'))
      compareProduct[me.number]=product
      me.setCompareProducts(compareProduct)
      me.keywork=product.ProductName
      me.productData=product
      sessionStorage.setItem('CompareProduct',JSON.stringify(compareProduct))
    },
    async querySearch(queryString, cb) {
      const param={
        queryText:queryString
      }
      await this.getProductAndProductAttribute(param)
        var products = this.products.map(item=>{
          item.value=item.Product.ProductName+" "+item.ProductAttribute.Memory+" GB"
          return item
        })
        

        // call callback function to return suggestions
        cb(products);
      },
      
      handleSelect(item) {
        
          
          item.Product.ProductAttributes=[]
          item.Product.ProductAttributes.push(item.ProductAttribute)
          item=item.Product
          
          
        this.chooseCompareProduct(item)
      },
    filterProduct(){
      const me=this
      const param={
        keyworkSearch:me.keywork
      }
      me.getProductAndProductAttribute(param)
    }
  },
};
</script>

<style lang="scss" scoped>
@import '@/styles/mixins/mixins.scss';

.compare-product-contain {
  width: 45%;
  .product-info {
    .product-image {
      img {
      }
    }
  }
  .recommand-product-area {
    .recommand-product {
      .product-item {
        @include border();
        .product-item-image {
          height: calc(100% - 21px);
        }
      }
    }
  }
}
</style>