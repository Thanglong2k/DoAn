<template>
  <div class="recommend-post-container">
    <area-card
      :title="'Bài viết về ' + product.ProductName"
      :showReviewAll="true"
      :type="$enumeration.AreaCardType.Post"
      @viewAllPost="viewAllPost"
    >
      <div
        class="
          post-item
          tw-flex tw-mt-3
          hover:tw-bg-gray-200
          tw-p-2 tw-cursor-pointer
        "
        v-for="(item, index) in posts"
        :key="index"
        @click="viewPostDetail(item.Post.PostID)"
      >
        <div class="avatar-post tw-w-12 tw-h-12 tw-mr-4">
          <img class="tw-w-full tw-h-full" :src="item.Post.Avatar" />
        </div>
        <div class="title-post tw-text-gray-600 tw-font-semibold">
          {{ item.Post.Title }}
        </div>
      </div>
    </area-card>
  </div>
</template>

<script>
import AreaCard from "@/components/controls/area-card";
import { mapActions, mapMutations, mapState } from "vuex";
export default {
  components: {
    AreaCard,
  },
  props: {
    product: {
      type: Object,
      default: () => {},
    },
    posts: {
      type: Array,
      default: () => [],
    },
  },
  methods: {
    ...mapMutations("product",["setNewProductPosts"]),
    viewAllPost() {
      const me = this;
      me.setNewProductPosts([])
      me.$router.push({
        path: "/posts",
        query: {
          product: me.product.ProductID,
        },
      });
    },
    viewPostDetail(postID) {
      const me = this;
      me.$router.push(`/post/${me.product.Meta}/${postID}`);
    },
  },
};
</script>

<style>
</style>