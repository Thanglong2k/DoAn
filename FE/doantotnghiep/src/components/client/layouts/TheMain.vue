<template>
<div class="main tw-relative">
   
   <router-view :key="$route.fullPath"></router-view>
   <div class="link-to tw-fixed tw-left-6 tw-bottom-12 tw-w-12 tw-cursor-pointer">
      <img :src="ic_Facebook" @click="directToFacebook"/>
   </div>
</div>
</template>
<script>
   export default{
      data(){
         return {
            ic_Facebook:require("@/assets/icons/facebook.png")
         }
      },
      methods:{
         directToFacebook(){
            const me=this
            window.open(
  'https://www.facebook.com/TTL-Shop-111944441520389',
  '_blank' // <- This is what makes it open in a new window.
);
            
         }
      }
   }
</script>
<style lang="scss" scoped>
.main{
   width: 75%;
   margin: 16px auto;
       min-height: calc(100% - 384px);
   .link-to{
      height:fit-content;
   }
}
</style>