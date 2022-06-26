<template>
  <div class="data-empty tw-bg-white tw-p-4">
      <div class="image tw-flex tw-justify-center">
          <img :src="imgEmpty"/>
      </div>
      <div class="text tw-text-center">
        <div>{{text}}</div>
        <div>{{$t('DataEmpty.Retry')}}</div>  
      </div>
  </div>
</template>

<script>
export default {
    props:{
        text:{
            type:String,
            default:'Rất tiếc, chúng tôi không tìm thấy dữ liệu bạn cần tìm!'
        }
    },
data(){
    return{
        imgEmpty:require('@/assets/images/empty-data/empty-data.png')
    }
}
}
</script>

<style>

</style>