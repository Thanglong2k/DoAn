<template>
  <InfoImport v-if="purchasedHistory.length===0"/>
  <PurchasedOrders v-else/>
</template>

<script>
  import {mapState,mapMutations} from 'vuex'
import InfoImport from '@/components/client/views/purchase-history/info-import.vue'
import PurchasedOrders from '@/components/client/views/purchase-history/purchased-orders.vue'
export default {
    components:{
        InfoImport,
        PurchasedOrders
    },
data(){
  return{
  }
},
computed:{
  ...mapState('order',['purchasedHistory']),
},
created(){
  const me=this
  me.setPurchasedHistory([])
},
methods:{
  ...mapMutations('order',['setPurchasedHistory'])
}
}
</script>

<style>

</style>