<template>
  <div class="review tw-mt-4">
    <area-card :title="$t('Rating.ProductReview', { number: reviewTotal })">
      <div class="review-info tw-flex tw-justify-around tw-w-full">
        <div class="average-rating">
          <div class="average-rating-title">
            {{ $t("DetailProduct.AvgReview") }}
          </div>
          <div class="rating-rate">{{ productSelected.Rate }}/5</div>
          <el-rate
            v-model="productSelected.Rate"
            disabled
            :allow-half="true"
          ></el-rate>
        </div>
        <div class="table-rating">
          <ReviewSliders :product="productSelected" />
        </div>
        <div class="writing-rating">
          <div class="question">
            {{ $t("DetailProduct.UsedThisProductYet") }}
          </div>
          <v-btn @click="onShowRating" class="rating-btn">{{
            $t("DetailProduct.SendYourReview")
          }}</v-btn>
        </div>
      </div>
      <div
        class="
          area-rating
          tw-w-full tw-flex tw-items-start tw-mt-6 tw-p-6 tw-bg-gray-100
        "
        v-if="showRating"
      >
        <div class="product-rating tw-w-2/5">
          <div class="rating-question">
            {{ $t("DetailProduct.HowManyStarsDoYouGiveThisProduct") }}
          </div>
          <el-rate
            v-model="rating"
            :texts="[
              $t('Rating.DontLike'),
              $t('Rating.Possible'),
              $t('Rating.Ordinary'),
              $t('Rating.Glad'),
              $t('Rating.Wonderful'),
            ]"
            show-text
          ></el-rate>
          <span class="text-error" v-if="showReviewError"
            >Vui lòng chọn đánh giá của bạn về sản phẩm này</span
          >
        </div>
        <div class="area-writing tw-flex-1 tw-ml-6 tw-relative">
          <!-- <v-textarea class="text-area" aria-placeholder="Bạn có lời khuyên gì cho cửa hàng" rows="3"></v-textarea> -->

          <el-input
            type="textarea"
            :autosize="{ minRows: 3 }"
            :placeholder="$t('DetailProduct.WhatAdviceDoYouHaveForTheStore')"
            v-model="content"
          >
          </el-input>
          <v-btn @click="onSendReview" class="send-btn">{{
            $t("DetailProduct.SendReview")
          }}</v-btn>
          <span class="text-error" v-if="showCommentError"
            >Mời viết bình luận (tối thiểu 3 kí tự)</span
          >
        </div>
      </div>
      <!-- Danh sách đánh giá của khách hàng -->
      <div class="review-result tw-mt-6">
        <div class="review-list">
          <div
            class="review-item tw-flex tw-mb-4"
            v-for="item in evaluations"
            :key="item.EvaluationID"
          >
            <div class="avatar-area tw-w-16 tw-h-16 tw-mr-4">
              <el-avatar shape="square">
                {{ nameAvatar(item.CustomerName) }}
              </el-avatar>
            </div>
            <div class="review-info tw-flex-1">
              <div class="user-name tw-font-semibold">
                {{ item.CustomerName }}
              </div>
              <div class="rating tw-flex">
                <el-rate v-model="item.Rate" disabled></el-rate>
                <div class="rating-time tw-text-sm tw-text-gray-400">
                  {{ $commonFn.setCommentTime(item.CreatedDate) }}
                </div>
              </div>
              <div class="content">
                {{ item.Content }}
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
    <ReviewCustomerInfoPopup
      ref="popup"
      @saveReview="saveReview"
      title="Hoàn thành gửi đánh giá"
      :type="$enumeration.CustomerInfoPopup.Review"
    />
  </div>
</template>

<script>
import ReviewCustomerInfoPopup from "../popup/customer-info-popup.vue";
import ReviewSliders from "./review-sliders.vue";
import AreaCard from "@/components/controls/area-card";
import { mapState, mapActions, mapMutations } from "vuex";
export default {
  components: {
    AreaCard,
    ReviewCustomerInfoPopup,
    ReviewSliders,
  },
  props: {
    product: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      showRating: false,
      model: 4,
      rating: 0,

      content: "",
      showReviewError: false,
      showCommentError: false,
      pageIndex: 1,
      pageSize: 5,
    };
  },
  computed: {
    ...mapState("evaluation", ["evaluations", "pageTotal", "reviewTotal"]),
    ...mapState("order", [
      "isCheckCustomerPurchasedProduct",
      "customerIDPurchased",
    ]),
    productSelected() {
      return this.product;
    },
  },
  async mounted() {
    const me = this;
    me.$commonFn.showMark("review");

    await me.getEvaluationOfProduct(1);
    me.$commonFn.hideMark("review");
  },
  methods: {
    ...mapActions("evaluation", ["getEvaluationByProductID"]),
    ...mapMutations("evaluation", ["setValueRating", "setContentRating"]),
    ...mapActions("order", ["checkCustomerPurchasedProduct"]),
    ...mapActions("evaluation", ["insertReviewOfProuct"]),
    /**
     * Hiển thị đánh giá
     */
    onShowRating() {
      const me = this;
      me.showRating = true;
    },
    /**
     * Gửi đánh giá
     */
    onSendReview() {
      const me = this;
      if (me.rating === 0) {
        me.showReviewError = true;
      }
      if (me.content.length < 3) {
        me.showCommentError = true;
      }
      if (!me.showReviewError && !me.showCommentError) {
        me.setValueRating(me.rating);
        me.setContentRating(me.content);
        me.$refs.popup.setModel(true);
      }
    },
    /**
     * Lưu đánh giá
     */
    async saveReview(modelForm) {
      const me = this;
      const paramCheck = {
        phoneNumber: modelForm.PhoneNumber,
        productAttributeID: me.productSelected.ProductAttributeID,
      };
      await me.checkCustomerPurchasedProduct(paramCheck);
      if (!me.isCheckCustomerPurchasedProduct) {
        me.$alert("Bạn chưa mua sản phẩm này!", "Cảnh báo", {
          confirmButtonText: "Đồng ý",
        });
      } else {
        const param = {
          FullName: modelForm.FullName,
          PhoneNumber: modelForm.PhoneNumber,
          Email: modelForm.Email,
        };
        const paramEvaluation = {
          ProductAttributeID: me.productSelected.ProductAttributeID,
          CustomerID: me.customerIDPurchased,
          Rate: me.rating,
          Content: me.content,
        };
        const h = this.$createElement;
        me.insertReviewOfProuct(paramEvaluation)
          .then(async (res) => {
            me.$commonFn.showMark("review");
            me.rating = 0;
            me.content = "";
            await me.getEvaluationOfProduct(1);
            me.$commonFn.hideMark("review");
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.InsertSuccess", { object: "đánh giá" })
              ),
              type: "success",
            });
          })
          .catch((e) => {
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
    },
    //set avatar đánh giá cho người dùng
    nameAvatar(name) {
      return this.$commonFn.takeTheFirstLetter(name);
    },
    /**
     * Lấy danh sách đánh giá của sản phẩm
     */
    getEvaluationOfProduct(pageIndex) {
      const me = this;
      const params = {
        productAttributeID: me.productSelected.ProductAttributeID,
        pageindex: pageIndex,
        pageSize: me.pageSize,
      };
      me.getEvaluationByProductID(params);
    },
  },
  watch: {
    rating(newVal) {
      const me = this;
      if (newVal != 0) {
        me.showReviewError = false;
      }
    },
    content(newVal) {
      const me = this;
      if (newVal.length >= 3) {
        me.showCommentError = false;
      }
    },
    async pageIndex() {
      const me = this;
      me.$commonFn.showMark("review");

      await me.getEvaluationOfProduct(me.pageIndex);
      me.$commonFn.hideMark("review");
    },
  },
};
</script>

<style scoped>
.send-btn {
  @apply tw-bg-red-500 tw-text-white tw-absolute tw-right-4 tw-top-4 !important;
}
.rating-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<style lang="scss" scoped>
@import "@/styles/mixins/mixins.scss";
.v-text-area text-area {
  border: $default-border;
}

::v-deep {
  .rating {
    .el-rate {
      .el-rate__item {
        .el-rate__icon {
          font-size: 14px !important;
        }
      }
    }
  }
  .el-textarea__inner {
    padding-right: 168px;
  }
}
.area-rating {
  .text-error {
    color: red;
    font-size: 13px;
  }
}
.review-result {
  .review-list {
    .review-item {
      .avatar-area {
        .el-avatar {
          height: 100%;
          width: 100%;
          line-height: 64px;
          font-size: 20px;
        }
      }
      .review-info {
      }
    }
  }
}
</style>