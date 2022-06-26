<template>
  <div class="follow-order-container">
      <InfoImport :type="$enumeration.TypeLookupOrder.FollowOrder" v-if="purchasedHistory.length===0 && !isSearchOrder"/>
      <PurchasedOrders v-else :type="$enumeration.TypeLookupOrder.FollowOrder"/>
  </div>
</template>

<script>
import {mapState,mapMutations} from 'vuex'
import InfoImport from '../purchase-history/info-import.vue'
import PurchasedOrders from '@/components/client/views/purchase-history/purchased-orders.vue'
export default {
    components:{
        InfoImport,
        PurchasedOrders
    },
    computed:{
  ...mapState('order',['purchasedHistory','phoneNumber','isSearchOrder']),
},
created(){
  const me=this
  me.setPhoneNumber('')
  me.setIsSearchOrder(false)
  me.setPurchasedHistory([])
},
methods:{
  ...mapMutations('order',['setPurchasedHistory','setPhoneNumber','setIsSearchOrder'])
}
}
</script>

<style>

</style>