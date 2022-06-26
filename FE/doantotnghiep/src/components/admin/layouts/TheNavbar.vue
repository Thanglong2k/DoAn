<template>
  <div class="navbar" :class="{ 'not-expend': isToggleNavbar }">
    <div class="navbar-header">
      <div class="navbar-logo" :class="{ display: isToggleNavbar }">
        <div class="logo"></div>
        <span class="text">TTL Admin</span>
      </div>
      <div class="navbar-toggle icon-svg" @click="toggleNavbar"></div>
    </div>
    <div class="navbar-menu">
      <div
        class="nav-item "
        v-for="(item, index) in navItems"
        :key="index"
        
        
      >
        <router-link class="menu-item tw-w-full tw-flex" v-if="item.link"
          :to="item.link"
        :title="item.text"
        @click.native="setHeader(item)"
        >
          <div class="menu-item-icon icon" :class="item.icon"></div>
          <div class="menu-item-text" :class="{ display: isToggleNavbar }">
            {{ item.text }}
          </div>
        </router-link>
        <div class="menu-item tw-w-full tw-flex tw-items-center" :class="[{'router-active':index===indexActive}]" v-if="!item.link" :title="item.text">
          <div class="menu-item-icon icon" :class="item.icon"></div>
          <div class="menu-item-text" :class="{ display: isToggleNavbar }">
            {{ item.text }}
          </div>
          <div
            class="icon-arrow tw-w-8 tw-h-10 tw-mr-2"
            :class="[{ display: isToggleNavbar }]"
            @click.prevent="expendSubMenu(index)"
          ></div>
        </div>
        <div class="sub-menu" v-if="!item.link&&index === subIndex">
          <router-link class="sub-item"  :to="sub.link" v-for="(sub,indexSub) in item.subMenu" :key="indexSub" @click.native="setHeader(sub)">

            <div class="sub-item-text" :class="{ display: isToggleNavbar }" v-if="isAdmin||(sub.link!=='/admin/account' && !isAdmin)">
              {{ sub.text }}
            </div>
          </router-link>
        
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapMutations } from "vuex";

export default {
  name: "Navbar",
  props: {
    navItems: {
      type: Array,
      default: () => [],
    },
  },

  data() {
    return {
      arrowDownIcon: require("@/assets/icons/arrow-down.png"),
      subIndex: -1,
      indexActive:-1,
      isAdmin:false
    };
  },
  computed: {
    ...mapState("admin", {
      isToggleNavbar: "toggleNavbar",
    }),
    
  },
  created(){
    const me=this
    const permission=JSON.parse(sessionStorage.getItem("Account"))?.Permission
    me.isAdmin=permission===1
  },
  methods: {
    ...mapMutations("admin", ["setToggleNavbar", "setHeaderTitle"]),
    /**
     * thu/mở navbar
     * CreatedBy: TTLONG (8/9/2021)
     */
    toggleNavbar() {
      this.setToggleNavbar();
    },
    setHeader(item) {
      const me = this;
      sessionStorage.setItem("HeaderTitle",item.text)
      me.setHeaderTitle(item.text);
    },
    expendSubMenu(index) {
      const me = this;
      if (me.subIndex === index) {
        me.subIndex = -1;
      } else {
        me.subIndex = index;
      }
    },
    activeNavbar(index){
      const me=this
      me.indexActive=index
    }
  },
};
</script>

<style lang="scss" scoped>
@import url("../../../assets/css/navbar.css");
.navbar {
  background-color: #24344b;
  width: 200px;
  height: 100vh;
  box-sizing: border-box;
  transition: width 0.5s;
  position: fixed !important;
  .navbar-header {
    width: 100%;
    height: 53px;
    box-sizing: border-box;
    display: flex;
    justify-content: space-between;
    align-items: center;
    cursor: pointer;
    .navbar-logo {
      min-width: 150px;
      height: 50px;
      display: flex;
      justify-content: flex-start;
      align-items: center;
      .logo {
        background-size: 35px;
        background-image: url("../../../assets/icons/ic_thietbi_56.svg");
        background-repeat: no-repeat;
        background-position: center;
        height: 100%;
        width: 35px;
        margin-left: 12px;
      }
      .text {
        height: 100%;
        padding-left: 8px;
        display: flex;
        align-items: center;
        font-weight: 600;
        font-size: 16px;
        color: #f3f4f6;
      }
    }
    .navbar-toggle {
      width: 50px;
      height: 100%;
      background-position: 0px 0px;
    }
  }
  .navbar-menu {
    width: 100%;
    height: calc(100% - 54px);
    box-sizing: border-box;
    padding-top: 12px;

    .nav-item {
      width: inherit;
      
      margin-bottom: 6px;

      // display: flex;
      box-sizing: border-box;
      cursor: pointer;
      text-decoration: none;
      
      .menu-item {
        height: 40px;
        border-left: 4px solid #24344b;
        margin-bottom: 4px;
        &:hover {
        background-color: #404f64;
        border-color: #404f64;
      }
        .menu-item-icon {
          width: 30px;
          height: 40px;
          background-position: center;
          margin: 0 8px;
        }
        .menu-item-text {
          flex: 1;
          height: 40px;
          display: flex;
          align-items: center;
          color: #c8c8c8;
          animation: slide-down 1s ease;
        }
        .icon-arrow {
          background-image: url("../../../assets/icons/arrow-down.png");
          background-repeat: no-repeat;
          background-position: center;
          background-size: 12px;
        }
      }
      .sub-menu {
        .sub-item:hover {
        background-color: #404f64;
        border-color: #404f64;
      }
        .sub-item-text {
          margin-left: 50px;
          height: 40px;
          display: flex;
          align-items: center;
          color: #c8c8c8;
          animation: slide-down 0.5s ease;
        }
      }
    }
  }
}
.router-link-exact-active.menu-item {
    border-left-color: #0dd469 !important;
    background-color: #404f64;
  
}
.router-link-exact-active .menu-item-text {
  color: #15c068 !important;
  font-family: OpenSansBold;
}
.router-link-active.menu-item {
    border-left-color: #0dd469 !important;
    background-color: #404f64;
  
}
.router-link-active .menu-item-text {
  color: #15c068 !important;
  font-family: OpenSansBold;
}

.not-expend {
  width: 50px;
}
.display {
  display: none !important;
}
.router-active{
  border-left-color: #0dd469 !important;
    background-color: #404f64;
}
@keyframes slide-down {
  0% {
    opacity: 0;
  }

  100% {
    opacity: 1;
  }
}
/* :style="[isToggleNavbar?{'display':'none'}:{'display':'block'}]" */
</style>