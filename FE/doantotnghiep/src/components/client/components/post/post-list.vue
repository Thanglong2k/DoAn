<template>
  <div class="post-list-container tw-flex" v-if="product">
    <div class="post-list-area tw-bg-white tw-p-5 tw-w-9/12">
      <div class="title-list">
        {{postTotal}} bài viết về "<span class="tw-text-gray-800 tw-font-bold"
          >{{product.ProductName}}</span
        >"
      </div>
      <div class="post-list">
        <div
          class="post tw-flex tw-mt-4"
          v-for="(item, index) in posts"
          :key="index"
        >
          <div class="avatar-post tw-mr-6 tw-w-56 tw-h-32 tw-cursor-pointer" @click="viewDetailPost(item.Post.PostID)">
            <img class="tw-w-full tw-h-full" :src="item.Post.Avatar"/>
          </div>
          <div class="content-post">
            <div class="post-title tw-text-gray-800 tw-font-bold tw-text-lg tw-cursor-pointer" @click="viewDetailPost(item.Post.PostID)">
              {{ item.Post.Title }}
            </div>
            <div class="post-description tw-text-xs tw-text-gray-400 tw-mt-3">
              {{ item.Post.Description }}
            </div>
            <div class="info-user-post tw-text-gray-400 tw-text-xs tw-mt-3">
              <span class="user-name">{{ item.Account.FullName }}</span>
              <span class="post-time">- {{ $commonFn.formatDate(new Date(item.Post.CreatedDate),'dd/MM/yyyy hh:mm') }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="reference tw-flex-1">
      <div class="product-of-post tw-bg-white tw-ml-6 tw-p-4">
        <div class="product-avatar tw-w-48 tw-h-48">
          <img class="tw-w-full tw-h-full" :src="product.Avatar" />
        </div>
        <div class="product-info">
          <div class="product-name tw-font-bold tw-text-blue-500">
            {{ product.ProductName }}
          </div>
          <div
            class="view-detail tw-text-xs tw-text-blue-400 tw-cursor-pointer"
            @click="viewDetailProduct"
          >
            Xem chi tiết
          </div>
        </div>
      </div>
      <div class="new-product-list tw-bg-white tw-ml-6 tw-p-4 tw-mt-6">
        <div
          class="
            list-title
            tw-border-b-2
            tw-border-gray-400
            tw-border-solid
            tw-text-gray-700
            tw-font-semibold
          "
        >
          Sản phẩm mới
        </div>
        <div class="product-list">
          <div
            class="
              product
              tw-flex
              hover:tw-bg-gray-100
              tw-cursor-pointer tw-mt-4
            "
            v-for="(item,index) in newProductPosts"
            :key="index"
            @click="viewProductPost(item)"
          >
            <div class="avatar-product tw-w-20 tw-h-20 tw-ml-3">
              <img class="tw-w-full tw-h-full" :src="item.Product.Avatar"/>
            </div>
            <div class="content-product tw-flex-1">
              <div class="product-name">{{item.Product.ProductName}}</div>
              <div class="post-quantity tw-text-xs tw-text-blue-400">
                {{item.PostQuantity}} bài viết
              </div>
            </div>
          </div>
          
          <div
            class="
              view-more
              tw-text-xs
              tw-text-blue-400
              tw-text-center
              tw-mt-3
              tw-cursor-pointer
            "
            @click="viewMoreProduct"
            v-if="newProductPosts.length<newProductTotal"
          >
            Xem thêm
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapMutations, mapState } from "vuex";
export default {
  data() {
    return {
      pageIndex: 1,
      pageSize: 5,
      pageIndexProduct:1,
      pageSizeProduct:5
    };
  },
  created() {
    const me = this;
    const params = {
      productID: me.$route.query.product,
      pageIndex: me.pageIndex,
      pageSize: me.pageSize,
    };
    me.getPostByProductID(params);
    me.getProductByID(me.$route.query.product);
    const paramNewProductPost={
      productID: me.$route.query.product,
      pageIndex: me.pageIndexProduct,
      pageSize: me.pageSizeProduct,
    }
    me.getNewProductPosts(paramNewProductPost)
  },
  computed: {
    ...mapState("post", ["posts","postTotal"]),
    ...mapState("product", ["product","newProductPosts","newProductTotal","newPageTotal"]),
  },
  methods: {
    ...mapActions("post", ["getPostByProductID"]),
    ...mapActions("product", ["getProductByID","getNewProductPosts"]),
    ...mapMutations("product",["setNewProductPosts"]),
    viewDetailProduct() {
      const me = this;
      localStorage.setItem("Id_Product", me.product.ProductID);
      me.$router.push({
        name: "ProductDetail",
        params: { productMeta: me.product.Meta },
      });
    },
    viewDetailPost(postID){
      const me = this;
      me.$router.push(`/post/${me.product.Meta}/${postID}`);
    },
    viewMoreProduct(){
      const me=this
      me.pageIndexProduct++
      const params={
      productID: me.$route.query.product,
      pageIndex: me.pageIndexProduct,
      pageSize: me.pageSizeProduct,
    }
    me.getNewProductPosts(params)
    },
    viewProductPost(product){
      const me = this;
      me.setNewProductPosts([])
      me.$router.push({
        path: "/posts",
        query: {
          product: product.Product.ProductID,
        },
      });
    }
  },
};
</script>

<style lang="scss" scoped>
.post-list-container{
  .post-list-area{
    .post-list{
      .post{
        .content-post{

        }
      }
    }
  }
}
</style>