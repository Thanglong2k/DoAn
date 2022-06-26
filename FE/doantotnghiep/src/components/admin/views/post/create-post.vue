<template>
  <div class="create-post-container tw-p-6">
    <div class="header-area">{{isCreate?'Tạo bài viết':'Sửa bài viết'}}</div>
    <div class="body-area">
      <el-form v-model="modelForm"
      label-width="132px"
        label-position="left"
      >
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          :label="$t('Post.Title')"
          prop="Title"
        >
          <el-input
            v-model="modelForm.Title"
            :placeholder="$t('Post.TitleInput')"
            @change="changeData"
          />
        </el-form-item>
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          :label="$t('Post.Description')"
          prop="Description"
        >
          <el-input
            v-model="modelForm.Description"
            :placeholder="$t('Post.DescriptionInput')"
            @change="changeData"
          />
        </el-form-item>
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          :label="$t('Post.Content')"
          prop="Content"
        >
          <VueEditor v-model="modelForm.Content" />
        </el-form-item>
        <el-form-item
          class="tw-flex-1 tw-mr-4 avatar-item"
          :label="$t('Post.Avatar')"
          prop="Avatar"
        >
          <div class="image-item tw-mr-1">
            <img width="50" height="50" :src="preview" />
          </div>
          <input
            ref="avatar"
            type="file"
            hidden
            accept=".jpg, .jpeg, .png, .gif, .webp"
            @change="previewAvatar"
          />
          <el-button @click="selectAvatar">{{
            $t("Button.SelectImage")
          }}</el-button>
        </el-form-item>

        <el-form-item
          class="tw-flex-1 tw-mr-4 product"
          :label="$t('Post.Product')"
          prop="ProductID"
        >
          <el-select
            v-model="modelForm.ProductID"
            :placeholder="$t('Post.ProductSelect')"
          >
            <el-option
              v-for="item in optionProducts"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            >
                <div class="item-product tw-flex">
                    <div class="product-avatar tw-h-8 tw-w-8 tw-mr-4">
                        <img class="tw-h-full tw-w-full" :src="item.image"/>
                    </div>
                    <div class="product-name">{{item.label}}</div>
                </div>
            </el-option>
          </el-select>
        </el-form-item>

      <el-form-item
      class="tw-flex-1 tw-mr-4"
          :label="$t('Post.Status')"
          prop="Status"
      >
        <el-checkbox v-model="modelForm.Status"/>
      </el-form-item>
      <el-form-item>
        <el-button @click="addOrUpdatePost">{{isCreate?'Thêm':'Sửa'}}</el-button>
      </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import { VueEditor } from "vue2-editor";
export default {
    components:{
        VueEditor
    },
  data() {
    return {
      modelForm: {},
      rules: {},
      preview:null,
      avatar:null
    };
  },
  computed: {
    ...mapState("product", ["products"]),
    ...mapState('account',['account']),
    ...mapState('post',['post']),
    optionProducts() {
      const me = this;
      return me.products?.map((item) => {
        return { value: item.ProductID, label: item.ProductName , image: item.Avatar};
      });
    },
    isCreate() {
      const me = this;
      return me.$route.query.create ? true : false;
    },
  },
  created() {
    const me = this;
    me.getProducts();
    
  },
  async mounted(){
    const me=this
    await me.getPostDetailByID({postID:me.$route.query.id,admin:true})
    me.modelForm=JSON.parse(JSON.stringify(me.post.Post))
    me.preview=me.modelForm.Avatar
  },
  methods: {
    ...mapActions("product", ["getProducts"]),
    ...mapActions("post", ["insertPostOfProduct","updatePostOfProduct","getPostDetailByID"]),
    ...mapActions('account',['getAccountByUserName']),
    previewAvatar(event) {
      var input = event.target;
      console.log(input.files);

      if (input.files) {
        var reader = new FileReader();
        reader.onload = (e) => {
          this.preview = e.target.result;
        };

        this.avatar = input.files[0];
        reader.readAsDataURL(this.avatar);
      }
    },
    selectAvatar() {
      this.$refs.avatar.click();
    },
    changeData(){},
    async addOrUpdatePost(){
        const me=this
        if (me.avatar !== null) {
            me.modelForm.AvatarFile = me.avatar;
          }
          const account=JSON.parse(sessionStorage.getItem("Account"))
          await me.getAccountByUserName({userName:account.UserName})
          me.modelForm.AccountID=me.account.AccountID
          let formData = new FormData();
          for (var item in me.modelForm) {
            if (me.modelForm[item] !== null) {
              if (
                (Array.isArray(me.modelForm[item]) ||
                  typeof me.modelForm[item] === "object") &&
                item !== "AvatarFile"
              ) {
                me.modelForm[item] = JSON.stringify(me.modelForm[item]);
              }
            
                formData.append(item, me.modelForm[item]);
              
            }
          }
          const h = this.$createElement;
          //gọi api
          if (me.isCreate) {
            me.insertPostOfProduct(formData, {
              headers: { "Content-Type": "multipart/form-data" },
            })
              .then((res) => {
                me.$notify({
                  title: me.$t("Notify.Success"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.InsertSuccess",{object:'bài viết'})
                  ),
                  type: "success",
                });
               
                me.preview = null;
                me.avatar = null;

              })
              .catch((e) => {
                me.$notify.error({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.HasError")
                  ),
                });
              });
          }else{
            const param = {
              id: me.modelForm.PostID,
              formData: formData,
            };
            me.updatePostOfProduct(param, {
              headers: { "Content-Type": "multipart/form-data" },
            })
              .then((res) => {
                me.$notify({
                  title: me.$t("Notify.Success"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.UpdateSuccess",{object:'bài viết'})
                  ),
                  type: "success",
                });
               
                me.preview = null;
                me.avatar = null;

              })
              .catch((e) => {
                me.$notify.error({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.HasError")
                  ),
                });
              });
          }
    }
  },
};
</script>

<style lang="scss" scoped>
::v-deep{
    .avatar-item{
        .el-form-item__content{
            display:flex !important;
        }
    }


}
</style>