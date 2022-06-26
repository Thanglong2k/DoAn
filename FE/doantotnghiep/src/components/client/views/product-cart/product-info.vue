<template>
  <div class="product-info tw-flex tw-justify-between tw-mt-4 tw-pb-4">
    <div class="image tw-w-20 tw-h-20">
      <el-image :src="dataItem.Image" fit="contain">
        <div slot="placeholder" class="image-slot">
          Loading<span class="dot">...</span>
        </div>
      </el-image>
    </div>
    <div class="product tw-ml-4 tw-flex-1">
      <div class="product-name">{{ dataItem.ProductName + " " + dataItem.ProductAttributes[0].Memory+"GB"}}</div>
    </div>
    <div class="quantity">
      <el-input-number
        class="input-number"
        v-model="num"
        @change="handleChange"
        :min="0"
        :max="100"
      ></el-input-number>
    </div>
    <div class="price tw-w-40 tw-ml-4 tw-px-4 tw-text-right">
      <div class="promotion-price tw-text-red-400 tw-font-semibold" v-if="isSale">
        {{ $commonFn.formatMoney(dataItem.ProductAttributes[0].PromotionPrice.toString()) }}
      </div>
      <div
        class="root-price"
        :class="[
          { 'tw-text-gray-300 tw-line-through': isSale },
          { 'tw-text-red-400 tw-font-semibold': !isSale },
        ]"
      >
        {{ $commonFn.formatMoney(dataItem.ProductAttributes[0].Price.toString()) }}
      </div>
    </div>
    <el-button
      class="delete-btn tw-h-8 tw-w-7"
      icon="el-icon-delete"
      @click="deleteProductCart"
    ></el-button>
  </div>
</template>

<script>
import {mapMutations} from 'vuex'
export default {
  props: {
    dataItem: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      src: "https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg",
      num: this.dataItem.ProductAttributes[0].Quantity,
    };
  },
  computed:{
    isSale(){
      const me=this
      return me.dataItem.ProductAttributes[0].PromotionPrice!==0? true: false
    }
  },
  methods: {
    ...mapMutations('cart',['setProductCartLength','setProductCart']),
    /**
     * Thay đổi số lượng
     */
    handleChange(value,old) {
      const me = this;
      const cart = JSON.parse(localStorage.getItem("productCart"));
      //nếu giảm số lượng về 0 thì xóa khỏi giỏ
      if (value === 0) {
        const newCart = cart.filter((item) => {
          return item.ProductID !== me.dataItem.ProductID;
        });

        localStorage.setItem("productCart", JSON.stringify(newCart));
        localStorage.setItem("productCartLength", newCart.length);
        me.setProductCartLength(newCart.length)
        me.setProductCart(newCart)
        me.$router.go();
      } else {
        if(value>me.dataItem.Quantity){
          const h = me.$createElement;
        me.$notify({
                title: me.$t("Notify.Warning"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.ProdyctQuantityNotEnough")
                ),
                type: "warning",
              });
              me.$nextTick(()=>{

                me.num=old
              })
        }else{

          const newCart = cart.map((item) => {
            if (item.ProductID === me.dataItem.ProductID) {
              item.ProductAttributes[0].Quantity = value;
            }
            return item;
          });
          localStorage.setItem("productCart", JSON.stringify(newCart));
          localStorage.setItem("productCartLength", newCart.length);
           me.setProductCartLength(newCart.length)
          me.setProductCart(newCart)
        }
      }
      
    },
    /**
     * Xóa sản phẩm trong giỏ
     */
    deleteProductCart() {
      const me = this;
      const cart = JSON.parse(localStorage.getItem("productCart"));
      const newCart = cart.filter((item) => {
        return item.ProductAttributes[0].ProductAttributeID !== me.dataItem.ProductAttributes[0].ProductAttributeID;
      });
      localStorage.setItem("productCart", JSON.stringify(newCart));
      localStorage.setItem("productCartLength", newCart.length);
       me.setProductCartLength(newCart.length)
        me.setProductCart(newCart)
      me.$router.go();
    },
  },
};
</script>
<style scoped>
.delete-btn {
  @apply tw-bg-red-400 tw-text-white tw-p-0 !important;
}
</style>
<style lang="scss" scoped>
::v-deep {
  .el-input-number {
    line-height: 20px;
    .el-input-number__decrease,
    .el-input-number__increase {
      width: 20px;
    }
    .el-input__inner {
      height: 22px;
      padding-left: 0px;
      padding-right: 0px;
    }
  }
}

.input-number {
  width: 100px !important ;
}
.product-info {
  border-bottom: 1px solid rgba(206, 207, 206, 0.877);
}
</style>