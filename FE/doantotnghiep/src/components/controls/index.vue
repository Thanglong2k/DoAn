<template>
<v-app>
  <area-card/>
<carousel/>
<el-dialog ref="dialog" title="Hoàn thành gửi đánh giá">
      <template #default>
          
          <el-input v-model="input" label="a" placeholder="Please input" />
    </template>
  </el-dialog>
  <el-button @click="modelDialog=true">showw</el-button>
  <div class="mb-10"></div>
  <ProductCart/>
  <ProductCarousel/>
  <compare-data class="tw-w-4/5 tw-mx-auto"/>
  </v-app>
</template>

<script>
import CompareData from "@/components/client/views/compare-product/compare-data.vue";
import AreaCard from './area-card'
import Carousel from './product-slider'
import ProductCarousel from './product-carousel'
import ProductCart from '@/components/client/components/product-cart'

export default {
components:{
    AreaCard,
    Carousel,
    ProductCart,
    ProductCarousel,
    CompareData
},
data(){
  return{
    modelDialog:false
  }
}
}
</script>

<style>

</style>