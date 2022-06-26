<template>
  <div id="app" v-if="isLogin">
    <Navbar :navItems="navItems"/>
    <div 
      class="main"
      :class="{'navbar-not-expend':isToggleNavbar}"
    >
      <Header/>
      <Main/>
    </div>
  </div>
  <Login v-else/>
</template>

<script>
import Header from './TheHeader.vue'
import Navbar from './TheNavbar.vue'
import Main from './TheMain.vue'
import Login from '../components/login'
import {mapState, mapMutations} from 'vuex'



export default {
  name: 'App',
  components: {
    Header,
    Navbar,
    Main,
    Login
    

  },
  data() {
    return {
      navItems: [
        { icon: "icon-overview", link:"/admin/overview", text: "Tổng quan" },
        { icon: "icon-equipment", link:"/admin/product", text: "Sản phẩm" },
        { icon: "icon-order", link:"/admin/manufacturer", text: "Hãng sản xuất" },
        { icon: "icon-shoppingregistration", link:"/admin/category", text: "Danh mục"},
        { icon: "icon-equipmentsearch", text: "Đơn hàng" ,
          subMenu:[
            {
              link:"/admin/order",
              text:"Đơn bán hàng"
            },
            {
              link:"/admin/receipt",
              text:"Đơn nhập hàng"
            },
          ]
        },
        { icon: "icon-setting", text: "Hệ thống" ,
          subMenu:[
            {
              link:"/admin/comment",
              text:"Bình luận"
            },
            {
              link:"/admin/evaluation",
              text:"Đánh giá"
            },
            {
              link:"/admin/post",
              text:"Bài viết"
            },
            {
              link:"/admin/account",
              text:"Tài khoản"
            },
            {
              link:"/admin/customer",
              text:"Khách hàng"
            },
            {
              link:"/admin/suplier",
              text:"Nhà cung cấp"
            },
            
          ]
        },
        // { icon: "icon-report", link:"/report", text: "Báo cáo" },
      ],
      
    }
  },
  computed:{
    ...mapState('account',['isLogin']),
    ...mapState('admin',{
      isToggleNavbar:'toggleNavbar'
    })
  },
  created(){
    const me=this
    if(!me.isLogin){
      if(sessionStorage.getItem('Account')!==""&&sessionStorage.getItem('Account')!==null){
        me.setIsLogin(true)
      }
    }
  },
  methods:{
    ...mapMutations('account',['setIsLogin'])
  }
}
</script>

<style>

#app {
  display: flex;
  justify-content: flex-start;

}
.main{
  width:calc(100vw - 201px);
  height:100vh;
  transition: all 0.5s;
  margin-left: 200px;
  
}
.navbar-not-expend{
  width:calc(100vw - 51px);
  margin-left: 50px;
  transition: all 0.5s;
}
@import url("../../../assets/css/main.css");
@import url("../../../assets/css/commons.css");
</style>
