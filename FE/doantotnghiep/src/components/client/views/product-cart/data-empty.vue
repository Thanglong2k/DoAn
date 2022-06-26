<template>
  <div class="data-empty tw-flex tw-flex-col tw-items-center tw-bg-white tw-p-6 tw-relative">
    <div class="header-right tw-text-sm tw-cursor-pointer tw-absolute tw-top-3 tw-right-3 hover:tw-text-blue-500" @click="openFollowOrder">
          Theo dõi đơn hàng của bạn
        </div>
      <img :src="srcCart"/>
      <p>Không có sản phẩm nào trong giỏ hàng</p>
      <el-button class="back-home-btn" @click="backHome">{{$t("Button.BackHome")}}</el-button>
  </div>
</template>

<script>
export default {
data(){
    return{
        srcCart:require('@/assets/images/empty-data/empty-cart.png')
    }
},
methods:{
    /**
     * Quay về trang chủ
     */
    backHome(){
        const me=this
        me.$router.push("/")
    },
    /**
     * Mở form theo dõi đơn hàng
     */
    openFollowOrder(){
      const me=this
      me.$router.push("/follow-order")
    }
}
}
</script>

<style scoped>
.back-home-btn{

    @apply tw-bg-red-400 tw-text-white !important;
}
</style>