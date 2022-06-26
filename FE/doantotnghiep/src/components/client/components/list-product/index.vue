<template>
  <div class="list-product-container tw-w-full tw-flex">
    <FilterProduct class="tw-w-1/5 tw-mr-4"/>
    <ProductList/>
  </div>
</template>

<script>
import FilterProduct from '@/components/client/views/list-product/filter.vue'
import ProductList from '@/components/client/views/list-product/product-list.vue'
import { mapMutations } from 'vuex';


export default {
  components: {
    FilterProduct,
    ProductList
  },
  data() {
    return {
      
    };
  },
  beforeRouteLeave (to,from,next){
    if(from.name==='Search'){

      this.setQueryText('')
      sessionStorage.removeItem('QUERYTEXT')
    }
    next();
  },
  methods:{
    ...mapMutations('product',['setQueryText'])
  }


};
</script>

<style>
</style>