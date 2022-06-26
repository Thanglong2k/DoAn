<template>
  <div class="compare-container tw-bg-white tw-w-full">
    <div
      class="
        header
        name-product
        tw-font-medium tw-text-2xl tw-text-center tw-bg-white tw-py-4
      "
    >
      So sánh {{products.length>0?products[0].ProductName +" "+ products[0].ProductAttributes[0].Memory:''}} VS {{products.length>1?compareProducts[1].ProductName+" "+ compareProducts[1].ProductAttributes[0].Memory:''}}
    </div>
    <div class="body-compare tw-w-4/5 tw-mx-auto tw-p-8 tw-flex tw-justify-center">
      <CompareSearch :number="0" :product="{...products.length>0?{...products[0]}:{}}"/>
      <div class="line tw-px-16 tw-inline-block tw-relative">
        <hr/>
        <span class="tw-text-2xl tw-font-medium tw-w-12 tw-h-12 tw-flex tw-justify-center tw-items-center tw-top-1/2 tw-left-1/2 tw-absolute tw-bg-white">VS</span>
      </div>
      <CompareSearch :number="1" :product="{...products.length>1?{...products[1]}:{}}"/>
    </div>
    <div class="compare-info tw-mb-6">
      <compare-data class="tw-w-4/5 tw-mx-auto" :compareProducts="products"  :manufacturers="manufacturers"/>
    </div>
  </div>
</template>

<script>
import CompareSearch from "@/components/client/views/compare-product/compare-product.vue";
import CompareData from "@/components/client/views/compare-product/compare-data.vue";
import { mapState, mapMutations,mapActions } from "vuex";
export default {
  components: {
    CompareSearch,
    CompareData,
  },
  data(){
    return{
      manufacturers:[]
    }
  },
  computed:{
    ...mapState("product", ["recommoandProducts","compareProducts"]),
    ...mapState('manufacturer',['manufacturer']),
    products(){
      return this.compareProducts
    }
  },

async mounted(){
  const me=this
  
  //nếu state compare không có sản phẩm thì lấy từ session xuống
  if(me.compareProducts.length===0){

    me.setCompareProducts(JSON.parse(sessionStorage.getItem('CompareProduct')))
  }
 //nếu chưa chọn sản phẩm so sánh thì quay ra trang chủ
 if(me.compareProducts.length===0){
   me.$router.push("/")
 }
 const params = {
      manufacturerID: me.compareProducts[0]?.ManufacturerID,
      price: me.compareProducts[0]?.Price,
      productID:me.compareProducts[0]?.ProductID
    };
    await me.getRecommendProducts(params);
    await me.getManufacturerByID(me.compareProducts[0]?.ManufacturerID)
    me.manufacturers.push(me.manufacturer)

},
// beforeUpdate(){
//   const me=this
//   me.compareProducts=JSON.parse(sessionStorage.getItem('CompareProduct'))
// },
// updated(){
//   const me=this
//   me.compareProducts=JSON.parse(sessionStorage.getItem('CompareProduct'))
// },
methods:{
  ...mapMutations("product", ["setCompareProducts"]),
  ...mapActions("product", ["getRecommendProducts"]),
  ...mapActions('manufacturer',['getManufacturerByID']),
},
watch:{
  products:{
    async handler(){
      const me=this
      me.manufacturers=[]
      for(let i=0;i<me.products.length;i++){
        await me.getManufacturerByID(me.products[i].ManufacturerID)
        me.manufacturers.push(me.manufacturer)
      }
    },
    deep:true
  }
}
};
</script>

<style lang="scss" scoped>
.compare-container {
  .name-product {
  }
  .body-compare {
    .line {
      hr {
        content: "";
        height: 100%;
        background: #e1e4e6;
        width: 1px;
        margin: 0 !important;
      }
      span{
        border: solid 1px #cbd1d6;
        border-radius: 50%;
        color: #939ca3;
        transform: translate(-50%, -50%);
      }
    }
  }
}
</style>