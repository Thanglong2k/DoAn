<template>
  <div class="cart tw-w-full">
    <div
      class="cart-container tw-w-full tw-mx-auto tw-px-6 tw-bg-white tw-py-4"
      v-if="productCart && productCart.length > 0"
    >
      <div class="cart-header tw-h-12 tw-flex tw-justify-between">
        <div class="header-left tw-text-xl tw-font-semibold">
          Có {{ productCart.length }} sản phẩm trong giỏ hàng
        </div>
        <div
          class="
            header-right
            tw-text-sm tw-cursor-pointer
            hover:tw-text-blue-500
          "
          @click="openFollowOrder"
        >
          Theo dõi đơn hàng của bạn
        </div>
      </div>
      <div class="cart-body">
        <ProductCart
          v-for="item in productCart"
          :key="item.ProductID"
          :dataItem="item"
        />
        <div class="price-area tw-w-full">
          <div class="payment-total tw-p-4 tw-w-2/5 tw-ml-auto">
            <div class="price-total tw-mt-1 tw-flex tw-justify-between">
              <span>Tổng tiền:</span
              ><span>{{ $commonFn.formatMoney(priceTotal.toString()) }}</span>
            </div>
            <div
              class="promotion-price-total tw-mt-1 tw-flex tw-justify-between"
            >
              <span>Giảm:</span
              ><span>{{
                promotionPrice !== 0
                  ? "- " +
                    $commonFn.formatMoney(
                      (priceTotal - promotionPrice).toString()
                    )
                  : 0
              }}</span>
            </div>
            <div
              class="payment tw-mt-1 tw-flex tw-justify-between tw-font-bold"
            >
              <span>Cần thanh toán:</span>
              <span>{{
                promotionPrice !== 0
                  ? $commonFn.formatMoney(promotionPrice.toString())
                  : $commonFn.formatMoney(priceTotal.toString())
              }}</span>
            </div>
          </div>
        </div>

        <div class="order-form tw-w-4/5 tw-mt-6">
          <el-form
            :model="modelForm"
            :rules="rules"
            ref="ruleForm"
            label-width="120px"
            label-position="top"
            class="demo-ruleForm"
          >
            <el-form-item prop="Gender">
              <el-radio-group v-model="modelForm.Gender">
                <el-radio :label="0">Anh</el-radio>
                <el-radio :label="1">Chị</el-radio>
              </el-radio-group>
            </el-form-item>
            <div class="tw-flex tw-justify-between">
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Input.FullName')"
                prop="FullName"
              >
                <el-input
                  v-model="modelForm.FullName"
                  :placeholder="$t('Input.FullNameInput')"
                />
              </el-form-item>
              <el-form-item
                class="tw-w-52"
                :label="$t('Input.PhoneNumber')"
                prop="PhoneNumber"
              >
                <el-input
                  v-model="modelForm.PhoneNumber"
                  :placeholder="$t('Input.PhoneNumberInput')"
                  @change="changePhoneNumber"
                />
              </el-form-item>
            </div>
            <el-form-item :label="$t('Input.Email')" prop="Email">
              <el-input
                v-model="modelForm.Email"
                :placeholder="$t('Input.EmailInput')"
              />
            </el-form-item>
            <el-form-item
              :label="$t('Input.DeliveryAddress')"
              prop="DeliveryAddress"
            >
              <el-input
                v-model="modelForm.DeliveryAddress"
                :placeholder="$t('Input.DeliveryAddressInput')"
              />
            </el-form-item>
            <el-form-item :label="$t('Input.Note')" prop="Note">
              <el-input
                type="textarea"
                v-model="modelForm.Note"
                :placeholder="$t('Input.NoteInput')"
              />
            </el-form-item>
          </el-form>
        </div>
      </div>
      <div class="cart-footer mt-4 tw-pt-5 tw-text-center">
        <el-button class="order-btn" @click="orderProduct"
          >Hoàn tất đặt hàng</el-button
        >
      </div>
    </div>
    <EmptyCart v-else />
    <SucessPopup
      :customer="customer"
      :productCart="productCartSuccess"
      ref="popup"
      v-if="showSuccess"
      @closePopup="closePopup"
    />
  </div>
</template>

<script>
import ProductCart from "@/components/client/views/product-cart/product-info.vue";
import EmptyCart from "@/components/client/views/product-cart/data-empty.vue";
import SucessPopup from "@/components/client/views/product-cart/sucess-popup";
import { mapState, mapActions, mapMutations } from "vuex";
export default {
  components: {
    ProductCart,
    EmptyCart,
    SucessPopup,
  },
  data() {
    return {
      modelForm: {
        Gender: 0,
      },
      rules: {
        FullName: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
          { min: 3, message: "Tên tối thiểu 3 kí tự", trigger: "blur" },
        ],
        PhoneNumber: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        Gender: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        Email: [
          // {validator:validateEmail,trigger: 'blur'}
        ],
        DeliveryAddress: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
      },
      showSuccess: false,
      productCartSuccess: null,
      hasCustomer: false,
    };
  },
  computed: {
    ...mapState("cart", ["productCart", "productCartLength"]),
    ...mapState("customer", ["customer"]),

    /**
     * Tổng tiền
     */
    priceTotal() {
      const me = this;
      return me.productCart.reduce((total, current) => {
        return (
          total +
          current.ProductAttributes[0].Price *
            current.ProductAttributes[0].Quantity
        );
      }, 0);
    },
    /**
     * Tổng tiền
     */
    promotionPrice() {
      const me = this;
      return me.productCart.reduce((total, current) => {
        return (
          total +
          current.ProductAttributes[0].PromotionPrice *
            current.ProductAttributes[0].Quantity
        );
      }, 0);
    },
  },
  created() {
    const me = this;

    if (
      me.productCart.length === 0 &&
      localStorage.getItem("productCart") != "" &&
      localStorage.getItem("productCart") != null
    ) {
      me.setProductCart(JSON.parse(localStorage.getItem("productCart")));
    }
  },
  methods: {
    ...mapMutations("cart", ["setProductCartLength", "setProductCart"]),
    ...mapActions("order", ["insertOrder"]),
    ...mapActions("customer", [
      "getCustomerByPhoneNumber",
      "insertCustomer",
      "updateCustomer",
    ]),
    orderProduct() {
      const me = this;
      me.$refs["ruleForm"].validate(async (valid) => {
        if (valid) {
          const h = this.$createElement;
          // await me.getCustomerByPhoneNumber({
          //   phoneNumber: me.modelForm.PhoneNumber,
          // });

          if (!me.hasCustomer) {
            const paramCustomer = {
              CustomerName: me.modelForm.FullName,
              Email: me.modelForm.Email,
              PhoneNumber: me.modelForm.PhoneNumber,
              Gender:
                me.modelForm.Gender === 0
                  ? me.$enumeration.Gender.Male
                  : me.$enumeration.Gender.FeMale,
            };
            const h = this.$createElement;
            await me
              .insertCustomer(paramCustomer)
              .then(async (res) => {
                if (res) {
                  await me.getCustomerByPhoneNumber({
                    phoneNumber: me.modelForm.PhoneNumber,
                  });
                  await me.insertNewOrder()
                } else {
                  this.$notify.error({
                    title: me.$t("Notify.Error"),
                    message: h(
                      "i",
                      { style: "color: teal" },
                      me.$t("Notify.HasError")
                    ),
                  });
                }
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
          } else {
            let typeChange = 0;
            let message = "Bạn có muốn cập nhật thông tin";
            if (me.modelForm.FullName !== me.customer.CustomerName) {
              message += " <b>Tên khách hàng</b>,";
            }
            if (me.modelForm.Gender !== Number(me.customer.Gender)) {
              message += " <b>Giới tính</b>,";
            }
            if (me.modelForm.Email !== me.customer.Email) {
              message += " <b>Email</b>,";
            }
            if (
              me.modelForm.FullName !== me.customer.CustomerName ||
              me.modelForm.Gender !== Number(me.customer.Gender) ||
              me.modelForm.Email !== me.customer.Email
            ) {
              typeChange = 1;
            }
            message = message.slice(0, message.length - 1);
            message += " đã thay đổi không?";
            if (typeChange === 1) {
              this.$confirm(message, "Thông tin", {
                confirmButtonText: "Đồng ý",
                cancelButtonText: "Hủy",
                type: "info",
                dangerouslyUseHTMLString: true,
              }).then(async () => {
                me.modelForm.Gender = !!me.modelForm.Gender;
                const params = {
                  id: me.customer.CustomerID,
                  customer: me.modelForm,
                };
                await me
                  .updateCustomer(params)
                  .then(async (res) => {
                    const h = this.$createElement;
                    console.log(res);
                    if (res.MISACode === "MISA-004") {
                      this.$notify.error({
                        title: me.$t("Notify.Error"),
                        message: h(
                          "i",
                          { style: "color: teal" },
                          me.$t("Notify.HasError")
                        ),
                      });
                    } else {
                      await me.getCustomerByPhoneNumber({
                        phoneNumber: me.modelForm.PhoneNumber,
                      });
                      await me.insertNewOrder()
                    }
                  })
                  .catch(() => {
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
              });
            }else{
              await me.insertNewOrder()
            }
          }
        }
      });
    },
    async insertNewOrder(){
      const me=this
      const h = this.$createElement;
      var params = {
                    order: {
                      CustomerID: me.customer.CustomerID,
                      TotalPrice: me.promotionPrice,
                      DeliveryAddress: me.modelForm.DeliveryAddress,
                      Note: me.modelForm.Note,
                    },
                    orderDetails: [
                      ...me.productCart.map((item) => {
                        return {
                          ProductAttributeID:
                            item.ProductAttributes[0].ProductAttributeID,
                          Quantity: item.ProductAttributes[0].Quantity,
                          UnitPrice: item.ProductAttributes[0].PromotionPrice
                            ? item.ProductAttributes[0].PromotionPrice
                            : item.ProductAttributes[0].Price,
                        };
                      }),
                    ],
                  };
                  await me
                    .insertOrder(params)
                    .then((res) => {
                      if (res) {
                        me.$notify({
                          title: me.$t("Notify.Success"),
                          message: h(
                            "i",
                            { style: "color: teal" },
                            me.$t("Notify.InsertSuccess", {
                              object: "đơn hàng",
                            })
                          ),
                          type: "success",
                        });
                        me.productCartSuccess = JSON.parse(
                          JSON.stringify(me.productCart)
                        );
                        me.setProductCart([]);
                        me.setProductCartLength(0);
                        localStorage.setItem("productCart", JSON.stringify([]));
                        localStorage.setItem("productCartLength", 0);
                        me.showSuccess = true;
                        me.$nextTick(() => {
                          console.log("next");
                          me.$refs.popup.setModel(true);
                        });
                      } else {
                        me.$notify({
                          title: me.$t("Notify.Error"),
                          message: h(
                            "i",
                            { style: "color: teal" },
                            me.$t("Notify.HasError")
                          ),
                          type: "error",
                        });
                      }
                    })
                    .catch((e) => {
                      me.$notify({
                        title: me.$t("Notify.Error"),
                        message: h(
                          "i",
                          { style: "color: teal" },
                          me.$t("Notify.HasError")
                        ),
                        type: "error",
                      });
                    });
    },

    closePopup() {
      const me = this;
      me.showSuccess = false;
    },
    /**
     * Mở form theo dõi đơn hàng
     */
    openFollowOrder() {
      const me = this;
      me.$router.push("/follow-order");
    },
    async changePhoneNumber() {
      const me = this;
      if (me.modelForm.PhoneNumber || me.modelForm.PhoneNumber !== "") {
        await me.getCustomerByPhoneNumber({
          phoneNumber: me.modelForm.PhoneNumber,
        });
        if(me.customer){
          me.modelForm = JSON.parse(JSON.stringify(me.customer));
        me.modelForm.Gender = me.modelForm.Gender ? 1 : 0;
        me.modelForm.FullName = me.modelForm.CustomerName;
        me.hasCustomer = true;
        }
        
      } else {
        me.hasCustomer = false;
      }
    },
  },
};
</script>
<style scoped>
.order-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<style lang="scss" scoped>
@import "@/styles/mixins/mixins.scss";

.cart-container {
  .cart-header {
    border-bottom: $default-border;
  }
  .cart-body {
    .order-form {
      .fullname,
      .phone-number {
        width: calc(50% - 8px);
      }
    }
    .price-area {
      border-bottom: $default-border;
      .payment-total {
        font-size: 13px;
      }
    }
  }
  .cart-footer {
    border-top: $default-border;
  }
}
</style>