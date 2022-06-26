<template>
  <el-dialog
  class="dialog-container"
    ref="dialog"
    title="Trả lời bình luận"
    width="600px"
    :lock-scroll="false"
    :visible.sync="modelDialog"
    @close="cancelFill"
  >
    <template #default>
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="130px"
        label-position="left"
        class="demo-ruleForm"
      >
        <el-form-item>
          <div class="root-content">
            <div class="label">Trả lời bình luận:</div>
            <div class="comment-content">{{comment.Comment.ParentCommentID?comment.ParentComment:comment.Comment.Content}}</div>
          </div>
        </el-form-item>
        <el-form-item>
          <div class="reply-content">
            <div class="label">Thêm bình luận:</div>
            <div class="reply-content">
              <el-input
                type="textarea"
                v-model="modelForm.Content"
              ></el-input>
            </div>
          </div>
        </el-form-item>
        
          
      </el-form>
      
    </template>
    <template #footer>
        
      <div class="button">
        <el-button @click="completeFill" class="complete-btn">{{
          $t("Button.Complete")
        }}</el-button>
        <el-button @click="cancelFill" class="cancel-btn">{{
          $t("Button.Cancel")
        }}</el-button>
      </div>
    </template>
  </el-dialog>
</template>

<script>
import {mapState, mapActions, mapMutations} from 'vuex'
export default {
  props:{
    comment:{
      type:Object,
      default:()=>{}
    }
  },
data() {
    return {
      modelDialog: false,
      modelForm: {},
      rules: {},
    };
  },
  computed:{
      ...mapState('account',['account'])
  },
  created(){
      const me=this
      
      
  },
  methods:{
      ...mapActions('account',['getAccountByUserName']),
      ...mapActions('comment',['insertCommentOfProduct']),
      setModel(value) {
      const me = this;
      me.modelDialog = value;
    },
    
    async completeFill(){
        const me=this
        const account=JSON.parse(sessionStorage.getItem("Account"))
        await me.getAccountByUserName({userName:account.UserName})
        me.modelForm.AccountID=me.account.AccountID
        me.modelForm.ProductAttributeID=me.comment.ProductAttributeID
        me.modelForm.ParentCommentID=me.comment.Comment.ParentCommentID?me.comment.Comment.ParentCommentID:me.comment.Comment.CommentID
        const h = this.$createElement;
        await me.insertCommentOfProduct(me.modelForm).then((res) => {
          this.$notify({
            title: me.$t("Notify.Success"),
            message: h(
              "i",
              { style: "color: teal" },
              me.$t("Notify.InsertSuccess", { object: "bình luận" })
            ),
            type: "success",
          });
        })
        .catch((e) => {
          this.$notify.error({
            title: me.$t("Notify.Error"),
            message: h("i", { style: "color: teal" }, me.$t("Notify.HasError")),
          });
        });
    },
    cancelFill(){
      const me=this
      me.modelForm=[]
      me.modelDialog=false
    }
  }
}
</script>

<style lang="scss" scoped>
::v-deep{
    .el-dialog{
        margin-top:5vh !important;
        &__body{
            height:560px;
            overflow: auto;
        }
    }
    .el-form-item__content{
        margin-left: 0px !important;
    }
}
.dialog-container{
    
}
</style>