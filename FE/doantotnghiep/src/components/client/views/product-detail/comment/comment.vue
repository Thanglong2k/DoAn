<template>
  <div class="comment-container tw-mt-4">
    <area-card :title="$t('DetailProduct.Comment', { number: commentTotal })">
      <div class="comment-area tw-relative">
        <el-input
          type="textarea"
          :autosize="{ minRows: 3 }"
          :placeholder="$t('DetailProduct.WriteComment')"
          v-model="commentContent"
        >
        </el-input>
        <v-btn @click="onSendComment" class="send-btn">{{
          $t("Button.Comment")
        }}</v-btn>
        <span class="text-error" v-if="showCommentError"
          >Mời viết bình luận (tối thiểu 3 kí tự)</span
        >
      </div>
      <!-- Danh sách đánh giá của khách hàng -->
      <div class="comment-result tw-mt-6">
        <div class="comment-list">
          <div
            class="comment-item"
            v-for="(item,index) in commentList"
            :key="index"
          >
            <div class="parent-comment tw-flex tw-mb-4">
              <div class="avatar-area tw-w-16 tw-h-16 tw-mr-4">
                <el-avatar shape="square">
                  {{ nameAvatar(item.CustomerName?item.CustomerName:item.AccountName) }}
                </el-avatar>
              </div>
              <div class="comment-info tw-flex-1">
                <div class="user-name tw-font-semibold">
                  {{ item.CustomerName }}
                </div>
                <div class="comment-time tw-text-xs tw-text-gray-400">
                  {{$commonFn.setCommentTime(item.Comment.CreatedDate)}}
                </div>
                <div class="content">
                  {{ item.Comment.Content }}
                </div>
                <div
                  class="
                    content
                    tw-text-xs
                    mt-2
                    tw-cursor-pointer tw-text-blue-400
                    tw-flex
                  "
                  
                >
                <div class="reply" @click="replyComment(item.Comment.CommentID)">

                  Trả lời
                </div>
                <div class="show-reply tw-ml-6" @click="onShowReply(item.Comment.CommentID)" v-if="item.Comment.ChildComment.length>0">
                  {{showReply===item.Comment.CommentID?"Ẩn bình luận":"Hiện bình luận"}}
                </div>
                </div>
              </div>
            </div>
            <div class="comment-reply-area tw-relative tw-ml-10 tw-mb-4"
              v-if="commentSelectedReply===item.Comment.CommentID"
            >
              <el-input
                type="textarea"
                :autosize="{ minRows: 3 }"
                :placeholder="$t('DetailProduct.WriteComment')"
                v-model="replyContent"
              >
              </el-input>
              <v-btn @click="onSendReply" class="send-btn">{{
                $t("Button.Comment")
              }}</v-btn>
              <span class="text-error" v-if="showReplyError"
                >Mời viết bình luận (tối thiểu 3 kí tự)</span
              >
            </div>
            <div class="child-comment"
            v-if="showReply===item.Comment.CommentID"
            >

              <div
                class=" child-item tw-flex tw-ml-10 tw-mb-4"
                v-for="child in item.Comment.ChildComment"
                :key="child.Comment.CategoryID"
              >
                <div class="avatar-area tw-w-16 tw-h-16 tw-mr-4">
                  <el-avatar shape="square">
                    {{ nameAvatar(child.CustomerName?child.CustomerName:child.AccountName) }}
                  </el-avatar>
                </div>
                <div class="comment-info tw-flex-1">
                  <div class="user-name tw-font-semibold">
                    {{ child.CustomerName?child.CustomerName:(child.AccountName+" (Admin)") }}
                  </div>
                  <div class="comment-time tw-text-xs tw-text-gray-400">
                    {{$commonFn.setCommentTime(child.Comment.CreatedDate)}}
                  </div>
                  <div class="content">
                    {{ child.Comment.Content }}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="panigation">
          <v-pagination
            v-if="pageTotal > 1"
            v-model="pageIndex"
            :length="pageTotal"
            :total-visible="7"
          ></v-pagination>
        </div>
      </div>
    </area-card>
    <CommentCustomerInfoPopup
      ref="popup"
      @saveComment="saveComment"
      title="Hoàn thành gửi bình luận"
      :content="commentContent"
      :type="$enumeration.CustomerInfoPopup.Comment"
    />
  </div>
</template>

<script>
import AreaCard from "@/components/controls/area-card";
import CommentCustomerInfoPopup from "../popup/customer-info-popup.vue";
import { mapState, mapActions, mapMutations } from "vuex";
export default {
  components: {
    AreaCard,
    CommentCustomerInfoPopup,
  },
  props: {
    product: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      commentContent: "",
      replyContent: "",
      showCommentError: false,
      showReplyError: false,
      showReply: '',
      pageIndex: 1,
      pageSize: 5,
      commentSelectedReply: "",
    };
  },
  computed: {
    ...mapState("comment", ["comments", "pageTotal", "commentTotal"]),
    ...mapState("customer", ["customer"]),
    commentList(){
      return this.comments
    }
  },
  async mounted() {
    const me = this;
    me.$commonFn.showMark("comment-container")

    await me.getCommentOfProduct(1);
    me.$productHub.$on("comment-added-to-product", me.onCommentAddedToProduct);
            me.$commonFn.hideMark("comment-container")
  },
  methods: {
    ...mapActions("comment", [
      "getCommentByProductID",
      "insertCommentOfProduct",
    ]),
    ...mapActions("customer", ["getCustomerByPhoneNumber", "insertCustomer"]),
    ...mapMutations("comment", ["setContentComment","setComments"]),
    //set avatar bình luận cho người dùng
    nameAvatar(name) {
      return this.$commonFn.takeTheFirstLetter(name);
    },
    
    /**
     * lưu bình luận
     */
    async saveComment(modelForm) {
      const me = this;
      await me.getCustomerByPhoneNumber({ phoneNumber: modelForm.PhoneNumber });
      if (!me.customer) {
        const paramCustomer = {
          CustomerName: modelForm.FullName,
          Email: modelForm.Email,
          PhoneNumber: modelForm.PhoneNumber,
        };
        await me
          .insertCustomer(paramCustomer)
          .then(async (res) => {
            
            await me.getCustomerByPhoneNumber({ phoneNumber: modelForm.PhoneNumber });
            
          })
          .catch((e) => {
            const h = this.$createElement;

            this.$notify.error({
              title: me.$t("Notify.Error"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.HasError")
              ),
            });
          });
      }
      const paramComment = {
        ProductAttributeID: me.product.ProductAttributeID,
        CustomerID: me.customer.CustomerID,
        
        ParentCommentID:me.commentSelectedReply?me.commentSelectedReply:undefined
      };
      if(me.commentSelectedReply!==''){
        paramComment.Content= me.replyContent
      }else{
        paramComment.Content= me.commentContent
      }
      const h = this.$createElement;
      await me
        .insertCommentOfProduct(paramComment)
        .then(async (res) => {
          me.$commonFn.showMark("comment-container")
            me.modelForm={}
            me.commentContent=''
            me.replyContent=''
            me.commentSelectedReply=''

    //await me.getCommentOfProduct(1);

            me.$commonFn.hideMark("comment-container")
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
    /**
     * Lấy danh sách bình luận của sản phẩm
     */
    getCommentOfProduct(pageIndex) {
      const me = this;
      const params = {
        productAttributeID: me.product.ProductAttributeID,
        pageindex: pageIndex,
        pageSize: me.pageSize,
      };
      me.getCommentByProductID(params);
    },
    /**
     * Gửi bình luận
     */
    onSendComment() {
      const me = this;
      if (me.commentContent.length < 3) {
        me.showCommentError = true;
      }
      if (!me.showCommentError) {
        me.setContentComment(me.content);
        me.$refs.popup.setModel(true);
      }
    },
    onSendReply() {
      const me = this;
      if (me.replyContent.length < 3) {
        me.showReplyError = true;
      }
      if (!me.showReplyError) {
        me.setContentComment(me.content);
        me.$refs.popup.setModel(true);
      }
    },
    replyComment(commentID) {
      const me=this
      me.commentSelectedReply = commentID;
    },
    onShowReply(commentID) {
      const me=this
      if(me.showReply !== commentID){

        me.showReply = commentID;
      }else{
        me.showReply=''
      }
    },
    onCommentAddedToProduct(comment){
      const me=this
      if(me.product.ProductAttributeID===comment[0].Comment.ProductAttributeID){
        const dataNotParent=comment.filter(item=>{
                    return !item.Comment.ParentCommentID
                })
                const dataHasParent=comment.filter(item=>{
                    return item.Comment.ParentCommentID
                })
                const dataFinal=dataNotParent.map(item=>{
                    const childComment=dataHasParent.filter(child=>{
                        return child.Comment.ParentCommentID===item.Comment.CommentID
                    })
                    item.Comment.ChildComment=childComment
                    return item
                })
        me.setComments(dataFinal)
      }
    }
    
  },
  beforeDestroy() {
    const me=this
      // Make sure to cleanup SignalR event handlers when removing the component
      me.$productHub.$off("comment-added-to-product", me.onCommentAddedToProduct);
      
    },
  watch: {
    commentContent(newVal) {
      const me = this;
      if (newVal.length >= 3) {
        me.showCommentError = false;
      }
    },
    replyContent(newVal) {
      const me = this;
      if (newVal.length >= 3) {
        me.showReplyError = false;
      }
    },
    async pageIndex() {
      const me = this;
      me.$commonFn.showMark("comment-container")

    await me.getCommentOfProduct(me.pageIndex);
            me.$commonFn.hideMark("comment-container")
    },
  },
};
</script>

<style scoped>
.send-btn {
  @apply tw-bg-red-500 tw-text-white tw-absolute tw-right-4 tw-top-4 !important;
}
</style>
<style lang="scss" scoped>
.comment-container {
  .comment-area {
    .text-error {
      color: red;
      font-size: 13px;
    }
  }
  .comment-result {
    .comment-list {
      .comment-item {
        .child-item{
          border-left:4px solid rgb(132, 129, 129);
          padding-left:12px;
        }
        .avatar-area {
          .el-avatar {
            height: 100%;
            width: 100%;
            line-height: 64px;
            font-size: 20px;
          }
        }
        .comment-info {
        }
      }
    }
  }
}
</style>