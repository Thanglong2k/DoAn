<template>
  <div class="header-container tw-h-16 tw-bg-red-500 tw-w-full">
    <div
      class="
        header-area
        tw-bg-transparent
        tw-flex
        tw-justify-between
        tw-items-center
        tw-h-full
        tw-w-3/4
        tw-mx-auto
        tw-my-0
      "
    >
      <div class="header-area-left tw-h-full tw-flex tw-items-center tw-w-1/2">
        <div class="header-area-left_logo tw-h-full">
          <a href="/">
            <img class="tw-h-full" :src="logo" />
          </a>
        </div>
        <!-- <div
          class="
            header-left_search
            tw-flex
            tw-relative
            tw-rounded-lg
            tw-overflow-hidden
            tw-h-4/5
            tw-flex-1
          "
        > -->
        <el-input
        class="
              input-search
              tw-h-4/5
              tw-text-base
              tw-w-full
            "
        v-model="querySearch"
            :placeholder="placeholderSearch"
        >
          <el-button class="tw-h-full search-btn" @click="searchProduct" slot="append" icon="el-icon-search"></el-button>
        </el-input>
          <!-- <v-btn class="btn-search tw-right-0" icon @click="searchProduct">
            <v-icon>mdi-magnify</v-icon>
          </v-btn> -->
      </div>
      <div class="header-area-right tw-flex tw-text-white tw-text-xs">
        <!-- <div class="header-right_menu tw-flex">
          <div
            class="
              menu-item
              tw-text-center tw-text-white tw-font-bold tw-min-w-min tw-w-20
            "
            v-for="item in menus"
            :key="item.id"
          >
            {{ item.name }}
          </div>
        </div> -->
        <div class="call tw-mr-4 tw-p-2 tw-flex">
          <div class="buy-history-icon  tw-flex tw-justify-center">
            <img :src="iconCall" width="30" height="20"/>
          </div>
          <div class="call-info">
            <div class="call-text">{{$t('Header.CallBuyProduct')}}</div>
            <div class="phone-number tw-font-bold">1800.1099</div>
          </div>
        </div>
        <div class="btn-header buy-history  tw-cursor-pointer tw-mr-4 tw-p-2" @click="openWarrantyLookup">
          <div class="buy-history-icon  tw-flex tw-justify-center">
            <img :src="iconBuyHistory" width="20" height="20"/>
          </div>
          <div class="buy-history-text">{{$t('Header.WarrantyLookup')}}</div>
        </div>
        <div class="btn-header warranty-lookup tw-cursor-pointer tw-mr-4 tw-p-2" @click="openPurchasedHistory">
          <div class="buy-history-icon  tw-flex tw-justify-center">
            <img :src="iconBuyHistory" width="20" height="20"/>
          </div>
          <div class="buy-history-text">{{$t('Header.BuyHistory')}}</div>
        </div>
        <div class=" btn-header cart tw-p-2" @click="openCart">
          <div class="cart-icon tw-flex tw-justify-center tw-relative">
            <img :src="iconCart" width="20" height="20"/>
            <div class="tag-number tw-absolute tw--top-2 tw-right-2 tw-bg-yellow-300" v-if="productCartLength>0">{{productCartLength}}</div>
          </div>
          <div class="cart-text">{{$t('Header.Cart')}}</div>
          
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";
export default {
  data() {
    return {
      menus: [
        { id: 0, name: "Home" },
        { id: 1, name: "About" },
        { id: 2, name: "Content" },
        { id: 3, name: "Contact" },
        { id: 4, name: "Login" },
      ],
      querySearch:'',
      placeholderSearch: this.$t("Input.Search"),
      logo: require("@/assets/images/logo/logo.png"),
      iconCart:require("@/assets/icons/header/shopping-cart.png"),
      iconBuyHistory:require("@/assets/icons/header/documents.png"),
      iconCall:require("@/assets/icons/header/telephone-call.png"),
    };
  },
  computed:{
    ...mapState('cart',['productCartLength']),
    ...mapState('product',['queryText']),
    
  },
  created(){
    const me=this
    if(me.productCartLength===0 && localStorage.getItem("productCartLength")!=""  && localStorage.getItem("productCartLength")!=null){
      
       me.setProductCartLength(parseInt(localStorage.getItem("productCartLength")))
        
      }
  },
  methods:{
    ...mapActions("product", ["getProductsByCategory"]),
    ...mapMutations("cart", ["setProductCartLength"]),
    ...mapMutations("product", ["setQueryText"]),
    //vào rỏ hàng
    openCart(){
      const me=this
      me.$router.push('/product-cart')
    },
    //Mở lịch sử mua hàng
    openPurchasedHistory(){
      const me=this
      me.$router.push('/purchase-history')
    },
    //Mở tra cứu bảo hành
    openWarrantyLookup(){
      const me=this
      me.$router.push('/warranty-lookup')
    },
    /**
     * Tìm kiếm sản phẩm
     */
    async searchProduct(){
      const me = this;
      me.setQueryText(me.querySearch)
      sessionStorage.setItem('QUERYTEXT',me.querySearch)
      const params = {
        categoryID: '',
        queryText:me.querySearch,
        filter:[],
        pageSize: 10,
        pageIndex: 1,
        sortBy: 0,
      };
      await me.getProductsByCategory(params);
      if(me.$route.fullPath!=='/search'){
        me.$router.push('/search')
      }
      
    }
  },
  watch:{
    queryText(){
      this.querySearch=this.queryText
    }
  }
};
</script>
<style scoped>

</style>
<style lang="scss" scoped>
::v-deep{
  .el-input.is-active .el-input__inner, .el-input__inner:focus{
    border-color: #F87171;
  }
  .el-input__inner{
    height:100%;
  }
}
.header-area {
  &-left {
    &_search {
      .input-search {
        white-space: nowrap;
        overflow: hidden !important;
        text-overflow: ellipsis;
      }
      .btn-search {
        position: absolute !important;
        height: 100% !important;
      }
    }
  }&-right{
    .btn-header{
      cursor: pointer;
      border-radius: 8px;
      &:hover{
        background-color: #F87171;
      }
    }
    .buy-history{
      
    }
    .cart{
     .cart-icon{
       .tag-number{
         border-radius: 50%;
         width: 16px;
         height:16px;
         text-align: center;
       }

     }
    }
  }
}
</style>