<template>
  <div class="info-import tw-bg-white tw-p-6">
      <img class="tw-mx-auto" :src="imgBG"/>
        <div class="title tw-text-center tw-mt-4 tw-mb-2">
            Tra cứu thông tin đơn hàng
        </div>
        <div class="phone-number tw-flex tw-justify-center">

          <el-input
          class="input"
                v-model="phoneNumber"
                :placeholder="$t('Nhập số điện thoại mua hàng')"
                prefix-icon="el-icon-mobile-phone"
              />
        </div>
        <div class="continue-btn tw-flex tw-justify-center tw-mt-4">
          <el-button class="button tw-w-1/2 " @click="lookUpPurchasedHistory">{{$t('Button.Continue')}}</el-button>
        </div>

  </div>
</template>

<script>
  import {mapState,mapActions,mapMutations} from 'vuex'
export default {
  props:{
    type:{
      type:Number,
      default:0
    }
  },
data(){
    return{
        imgBG:require("@/assets/images/clients/purchase-history/background.png"),
        phoneNumber:""
    }
},
computed:{
  ...mapState('order',['purchasedHistory']),
},
methods:{
  ...mapActions('order',['getPurchasedHistory','getFollowOrder']),
  ...mapActions('customer',['getCustomerByPhoneNumber']),
  ...mapMutations('order',['setIsSearchOrder','setPhoneNumber']),
  async lookUpPurchasedHistory(){
    const me=this
    me.setPhoneNumber(me.phoneNumber)
    if(me.type===me.$enumeration.TypeLookupOrder.PurchasedHistory){

      await me.getPurchasedHistory(me.phoneNumber)
    }else{
      await me.getFollowOrder({phoneNumber:me.phoneNumber})
    }
    await me.getCustomerByPhoneNumber({phoneNumber:me.phoneNumber})
    if(!me.purchasedHistory.length>0){
      const h = this.$createElement;
      me.$notify.warning({
          title: me.$t("Notify.Warning"),
          message: h(
            "i",
            { style: "color: teal" },
            me.$t("Notify.PhoneNumberNotPurchaseOrder")
          ),
        });
    }else{
      if(me.type!==me.$enumeration.TypeLookupOrder.PurchasedHistory){{

        me.setIsSearchOrder(true)
      }}
    }
  }
}
}
</script>

<style>

  .input{
    @apply tw-w-2/4 !important;
  }
  .button{
    @apply tw-bg-red-500 tw-text-white !important;
  }

</style>