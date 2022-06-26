<template>
  <div class="header-page tw-sticky tw-top-0 tw-bg-white tw-z-50">
    <div class="header-left">
      <div class="header-text">{{headerTitle}}</div>
      <div class="header-icon"></div>
    </div>
    <div class="header-right">
      <div class="month tw-mr-6" v-if="$route.path==='/admin/overview'">
        <span class="tw-mr-4">Tháng</span>
        <el-select class="tw-w-28" v-model="valueMonth" placeholder="Tháng" @change="changeMonth">
          <el-option
      v-for="item in optionMonths"
      :key="item.value"
      :label="item.label"
      :value="item.value">
    </el-option>
        </el-select>
      </div>
      <div class="notification tw-cursor-pointer tw-relative" @click="onShowNotify">
        <div class="not-view-total tw-absolute tw-top-0 tw--right-2 tw-bg-red-500 tw-text-white tw-w-5 tw-h-5 tw-flex tw-justify-center tw-items-center" v-if="notifyNotViewTotal>0">{{notifyNotViewTotal>99?'99+':notifyNotViewTotal}}</div>
        <div class="arrow tw-h-5 tw-w-5 tw-absolute tw-border-gray-300 tw-bg-white tw-top-10 tw-z-10" v-if="showNotify"></div>
        <div class="notify-container tw-rounded-lg tw-absolute tw-border-gray-300 tw-top-12 tw--left-48 tw-w-96 tw-bg-white"  v-if="showNotify">
          <div class="notify-item tw-border-gray-200 tw-cursor-pointer tw-p-4 hover:tw-bg-gray-100"
          :class="[{'tw-bg-gray-200':!item.Notify.IsRead}]"
            v-for="item in notifies"
            :key="item.Notify.NotifyID"
            v-html="getNotifiesText(item)"
            @click="onHandleClickNotify(item)"
          ></div>
          
          
        </div>
      </div>
      <div class="user-avatar"></div>
      
      <div class="user tw-flex tw-h-full tw-items-center tw-relative">
        <div class="user-name">{{fullName}}</div>
      <div class="arrow" @click="showOption=!showOption">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="10"
          height="6.21"
          viewBox="0 0 9 5.21"
        >
          <path
            d="M.5.5l4,4,4-4"
            style="
              fill: none;
              stroke: #aaa;
              stroke-linecap: round;
              stroke-width: 1.2px;
            "
          ></path>
        </svg>
        
      </div>
      <div class="option-list tw-bg-white tw-cursor-pointer tw-w-28 tw-absolute tw-left-5 tw-top-12" v-if="showOption">
          <div class="item tw-py-2 tw-px-1" @click="logout">Đăng xuất</div>
        </div>
      </div>
      
    </div>
  </div>
</template>

<script>
import {mapState,mapMutations, mapActions} from 'vuex'
export default {
  name: "Header",
  data(){
    return{
      showOption:false,
      fullName:'',
      optionMonths:[
        {value:1,label:'Tháng 1'},
        {value:2,label:'Tháng 2'},
        {value:3,label:'Tháng 3'},
        {value:4,label:'Tháng 4'},
        {value:5,label:'Tháng 5'},
        {value:6,label:'Tháng 6'},
        {value:7,label:'Tháng 7'},
        {value:8,label:'Tháng 8'},
        {value:9,label:'Tháng 9'},
        {value:10,label:'Tháng 10'},
        {value:11,label:'Tháng 11'},
        {value:12,label:'Tháng 12'}
      ],
      valueMonth:new Date().getMonth()+1,
      showNotify:false
    }
  },
  computed:{
    ...mapState('admin',['headerTitle']),
    ...mapState('account',['account']),
    ...mapState('notify',['notifies','notifyNotViewTotal']),
    ...mapState('customer',['customer']),
    ...mapState('product',['product']),

  },
  mounted(){
    const me=this
    if(me.headerTitle===''){
      me.setHeaderTitle(JSON.parse(JSON.stringify(sessionStorage.getItem("HeaderTitle"))))
    }
    if(!me.account){
      me.fullName=JSON.parse(sessionStorage.getItem("Account"))?.FullName
    }else{
      me.fullName=me.account.FullName
    }
    me.setMonth(me.valueMonth)
    me.getNotifies()
    me.getNotifyNotViewTotal()
    //me.getAccountByUserName({userName:JSON.parse(sessionStorage.getItem("Account")).UserName})
  },
  methods:{
    ...mapMutations('account',['setIsLogin']),
    ...mapMutations('admin',['setHeaderTitle']),
    ...mapMutations('overview',['setMonth']),
    ...mapActions('notify',['getNotifies','getNotifyNotViewTotal','updateIsView','updateIsRead']),
    ...mapActions('account',['getAccountByUserName']),
    ...mapActions('customer',['getCustomerByID']),
    ...mapActions('product',['getProductAttributeByID']),
    logout(){
      const me=this
      me.setIsLogin(false)
      sessionStorage.removeItem('Account')
      me.$router.push('/login')
    },
    changeMonth(){
      const me=this
      me.setMonth(me.valueMonth)
    },
    onShowNotify(){
      const me=this
      me.showNotify=!me.showNotify
      me.updateIsView().then(res=>{
        me.getNotifyNotViewTotal()
      })
    },
    getNotifiesText(notify){
      const me=this
      
      //   me.getCustomerByID({CustomerID:notify.CustomerID})
      // me.getProductAttributeByID({productAttributeID:notify.ProductAttributeID})
      let t=""
      switch(notify.Notify.Type){
        case me.$enumeration.Notify.Comment:
          t=me.$t("Notifies.Comment",{customer:notify.Customer.CustomerName,product:notify.Product.ProductName+" "+ notify.ProductAttribute.Memory+"GB"})
          break
        case me.$enumeration.Notify.Evaluation:
          t= me.$t("Notifies.Evaluation",{customer:notify.Customer.CustomerName,product:notify.Product.ProductName+" "+ notify.ProductAttribute.Memory+"GB"})
          break
        case me.$enumeration.Notify.Order:
          t= me.$t("Notifies.Order",{customer:notify.Customer.CustomerName,order:notify.Order.OrderID})
          break
          default:
            break
      }
      return t
      
      
      
    },
    onHandleClickNotify(notify){
      const me=this
      const params={
        notifyID:notify.Notify.NotifyID
      }
      me.updateIsRead(params).then(async res=>{
        await me.getNotifies()
        switch(notify.Notify.Type){
        case me.$enumeration.Notify.Comment:
          me.$router.push("/admin/comment")
          return
        case me.$enumeration.Notify.Evaluation:
          me.$router.push("/admin/evaluation")
          return
        case me.$enumeration.Notify.Order:
          me.$router.push("/admin/order")
          return
      }
      })
      
    }
  }
};
</script>

<style lang="scss" scoped>
.header-page {
  width: 100%;
  height: 48px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 10px 10px rgb(0 0 0 / 20%);
  padding: 6px 16px;
  box-sizing: border-box;
  border-bottom:1px solid rgb(221, 220, 220);
  .header-left {
    display: flex;
    align-items: center;
    .header-text {
      font-size: 16px;
      font-family: OpenSansSemiBold;
    }
    .header-icon {
      background-image: url("../../../assets/icons/ic_Help_2_16.svg");
      background-repeat: no-repeat;
      background-position: center;
      width: 16px;
      height: 16px;
      margin-left: 5px;
    }
  }
  .header-right {
    height: 100%;
    display: flex;
    justify-content: flex-end;
    align-items: center;
    .notification {
      width: 22px;
      height: 100%;
      margin-right: 16px;
      background-image: url("../../../assets/icons/notification.svg") !important;
      background-repeat: no-repeat;
      background-position: center;
      .not-view-total{
        border-radius: 50%;
        font-size: 10px;
      }
      .arrow{
    //     border-left: 20px solid transparent;
    // border-right: 20px solid transparent;    
    // border-bottom: 20px solid white;
    border-bottom-width:0px !important;
    border-right-width:0px !important;
    border-width: 1px;
    border-style: solid;
    transform: rotate(45deg);
      }
      .notify-container{
        border-width: 1px;
        border-style: solid;
        .notify-item{
          border-bottom-width: 1px;
        border-bottom-style: solid;
        }
      }
    }
    .user-name {
      border-left: 1px solid rgb(187, 184, 184);
      padding: 0 16px;
      height: 100%;
      display: flex;
      justify-content: flex-end;
      align-items: center;
    }
    .user-avatar {
      background-image: url("../../../assets/icons/MISA.QLTH.ImageHandler.png");
      background-repeat: no-repeat;
      background-position: center;
      background-size: 40px;
      width: 40px;
      height: 100%;
      margin-left: 8px;
    }
    .option-list{
      z-index: 1000;
      box-shadow: 0px 0px 2px rgb(167, 164, 164);
    }
  }
}
</style>