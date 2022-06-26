<template>
<div class="tag
      
      tw-w-14
      tw-h-8
      tw-flex
      tw-items-center
      tw-justify-center
      "
      :class="[{'tw-bg-red-600 tw-text-white':discount>0 && !newProduct},
        {'tw-bg-yellow-300 tw-text-red-600':newProduct}
      ]"
      >
      {{text }}
  </div>
  
  
</template>

<script>
export default {
  props: {
    discount: {
      type: Number,
      default: 0,
    },
    newProduct:{
      type: Boolean,
      default: false,
    }
  },
  computed:{
      text(){
        let result=''
        if(this.discount>0 && !this.newProduct){
          result= `- ${this.discount}%`
        } 
        if(this.newProduct){
           result= 'Mới'
        }
        return result
      }
  },
  created(){
    
  }
};
</script>

<style scoped>
.tag {
  border-radius: 10px;
}
</style>