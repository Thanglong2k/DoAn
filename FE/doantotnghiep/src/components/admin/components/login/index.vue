<template>
  <div class="login-container tw-bg-red-300 tw-flex tw-items-center tw-justify-center">
    <div
      class="
        login-form tw-bg-white tw-rounded-lg tw-flex tw-flex-col tw-items-center tw-p-8
      "
    >
      <div class="login-title tw-text-red-500 tw-font-bold tw-text-3xl">
        ĐĂNG NHẬP
      </div>
      <span class="tw-mt-3 tw-text-red-600">{{ msgError }}</span>
      <div class="login-body tw-w-full">
        <el-form
          :model="modelForm"
          :rules="rules"
          ref="ruleForm"
          label-width="120px"
          label-position="top"
          class="demo-ruleForm"
        >
          <el-form-item class="item-form" :label="$t('Input.UserName')" prop="UserName">
            <!-- <label>{{$t('Input.FullName')}}<span class="tw-text-red-400">*</span></label> -->
            <el-input
              v-model="modelForm.UserName"
              :placeholder="$t('Input.UserNameInput')"
            />
          </el-form-item>
          <el-form-item class="item-form" :label="$t('Input.Password')" prop="Password">
            <!-- <label>{{$t('Input.PhoneNumber')}}<span class="tw-text-red-400">*</span></label> -->
            <el-input
              v-model="modelForm.Password"
              :placeholder="$t('Input.PasswordInput')"
              type="password"
              show-password
            />
          </el-form-item>
          <el-form-item class="item-form">
            <!-- <label>{{$t('Input.PhoneNumber')}}<span class="tw-text-red-400">*</span></label> -->
            <el-checkbox v-model="rememberPassword">Nhớ mật khẩu</el-checkbox>
          </el-form-item>
        </el-form>
        <div class="button tw-w-full tw-flex tw-justify-center tw-mt-6">
          <el-button class="login-btn" @click="handleLogin"
            >Đăng nhập</el-button
          >
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  data() {
    return {
      modelForm: {},
      rules: {
        UserName: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
          { min: 3, message: "Tên tối thiểu 3 kí tự", trigger: "blur" },
        ],
        Password: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
      },
      msgError:'',
      rememberPassword:false
    };
  },
  computed: {
    ...mapState("account", ["statusAccount","account"]),
  },
  created(){
    const me=this
    const accountLocal= JSON.parse(localStorage.getItem("login"))
    if(accountLocal.usename && accountLocal.password){
      me.modelForm.UserName=atob(accountLocal.usename)
      me.modelForm.Password=atob(accountLocal.password)
      me.rememberPassword=true
    }
  },
  methods: {
    ...mapActions("account", ["login"]),
    /**
     * Xử lý đăng nhập
     */
    async handleLogin() {
      const me = this;
      let sessAccount={}
      await me.login(me.modelForm).then((res)=>{
      });
      switch (me.statusAccount) {
        case me.$constant.Login.NotExists:
          me.msgError = "Tài khoản hoặc mật khẩu không đúng";
          break;
        case me.$constant.Login.Locked:
          me.msgError = "Tài khoản đã bị khóa";
          break;
        default:
          sessAccount={
            UserName:me.account.UserName,
            FullName:me.account.FullName,
            Permission:me.account.Permission
          }
          await sessionStorage.setItem('Account',JSON.stringify(sessAccount))
          if(me.rememberPassword){
            const account={
              usename:btoa(me.modelForm.UserName),
              password:btoa(me.modelForm.Password),
              remember:true
            }
            localStorage.setItem('login',JSON.stringify(account))
          }else{
            localStorage.removeItem('login')
          }
          
          if(me.$route.fullPath==='/login'){
            await me.$router.push('/admin/overview');
          }else{
            
            me.$router.go();
          }
          break;
      }
      
    }
  },
};
</script>
<style scoped>
.login-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<style lang="scss" scoped>
.login-container {
  height:100vh;
  .login-form {
        box-shadow: 0px 0px 10px -1px white;
    width: 500px;
    .login-body{
      .item-form{
        margin-bottom: 0px;
        .el-form-item_label{
          padding:0;
        }
      }
    }
  }
}
</style>