<template>
  <div class="create-receipt-container">
    <div class="header">
      {{
        isCreate ? $t("Receipt.AddNewReceipt") : $t("Receipt.EditNewReceipt")
      }}
    </div>

    <div class="form-create tw-p-6">
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="120px"
        label-position="left"
        class="demo-ruleForm tw-grid tw-grid-cols-2"
      >
        <div class="form-left tw-col-span-1">
          <el-form-item class="tw-flex-1 tw-mr-4" :label="$t('Receipt.Supier')">
            <el-select
              v-model="valueSupier"
              :placeholder="$t('Receipt.Supier')"
            >
              <el-option
                v-for="item in optionSupiers"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item
            class="tw-flex-1 tw-mr-4"
            :label="$t('Receipt.PaymentStatus')"
            prop="PaymentStatus"
          >
            <el-checkbox v-model="modelForm.PaymentStatus">
              {{ $t("Receipt.Paid") }}
            </el-checkbox>
          </el-form-item>
        </div>
        <div class="form-right tw-col-span-1">
          <el-form-item
            class="tw-flex-1 tw-mr-4"
            :label="$t('Receipt.Note')"
            prop="Note"
          >
            <el-input
              type="textarea"
              v-model="modelForm.Note"
              :placeholder="$t('Receipt.NoteInput')"
            />
          </el-form-item>
          <el-form-item
            class="tw-flex-1 tw-mr-4"

            :label="$t('Receipt.TotalMoney')"
            prop="TotalMoney"
          >
            <el-input
              :disabled="true"
              v-model="modelForm.TotalMoney"
              :placeholder="$t('Receipt.TotalMoney')"
            />
          </el-form-item>
        </div>
      </el-form>
      <el-button @click="selectProduct">Chọn sản phẩm</el-button>
    </div>
    <div class="receipt-detail-list">
      <el-table
        class="table"
        :data="productList"
        height="calc(100% - 49px - 16px)"
        style="width: 100%"
      >
        <el-table-column type="index" label="STT" width="50"> </el-table-column>
        <el-table-column
          prop="Product.ProductName"
          label="Sản phẩm"
          min-width="150"
        >
        </el-table-column>
        <el-table-column prop="Product.Avatar" label="Ảnh" width="100">
          <template slot-scope="scope">
            <img :src="scope.row.Product.Avatar" />
          </template>
        </el-table-column>
        <el-table-column prop="ReceiptDetail.Quantity" label="Số lượng" width="150">
          <template slot-scope="scope">
            <el-input v-model="scope.row.ReceiptDetail.Quantity" />
          </template>
        </el-table-column>
        <el-table-column prop="ReceiptDetail.UnitPrice" label="Đơn giá" width="150">
          <template slot-scope="scope">
            <el-input v-model="scope.row.ReceiptDetail.UnitPrice" />
          </template>
        </el-table-column>
        <el-table-column prop="ReceiptDetail.Note" label="Ghi chú" width="150">
          <template slot-scope="scope">
            <el-input type="textarea" v-model="scope.row.ReceiptDetail.Note" />
          </template>
        </el-table-column>

        <el-table-column label="Chức năng" width="120">
          <template slot-scope="scope">
            <el-button
              @click="
                handleDelete(scope.row.ProductAttribute.ProductAttributeID)
              "
              type="text"
              size="small"
              >{{ $t("Button.Delete") }}</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </div>
    <el-button class="add-btn" @click="addOrUpdateReceipt">{{
      isCreate ? $t("Button.Add") : $t("Button.Edit")
    }}</el-button>

    <ProductSelectPopup
      ref="popup"
      @setSelectedProducts="setSelectedProducts"
    />
  </div>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";
import ProductSelectPopup from "./popup/products-select.vue";
export default {
  components: {
    ProductSelectPopup,
  },
  data() {
    return {
      modelForm: {},
      valueSupier: "",
      valueCategory: "",
      rules: {},
      inputVisible: false,
      inputValue: "",
      showDetail: false,
      productList: [],
      showComponent: false,
    };
  },
  computed: {
    ...mapState("suplier", ["supliers"]),
    ...mapState("category", ["categoryAll"]),
    ...mapState("receipt", ["receipt"]),
    /**
     * Giá trị option chọn hãng sản xuất
     */
    optionSupiers() {
      const me = this;
      return me.supliers?.map((item) => {
        return { value: item.SuplierID, label: item.SuplierName };
      });
    },

    isCreate() {
      const me = this;

      return me.$route.query.create ? true : false;
    },
  },
  async mounted() {
    const me = this;
    await me.getSupliers();
    //nếu là update thì lấy data
    if (!me.isCreate) {
      await me.getReceiptByID(me.$route.query.receiptID).then((res) => {
        if (me.receipt) {
          me.modelForm = JSON.parse(JSON.stringify(me.receipt.Receipt));

          me.valueSupier = me.modelForm.SuplierID;
          me.productList = me.receipt.ReceiptDetails;
        }
      });
    }
  },

  methods: {
    ...mapActions("suplier", ["getSupliers"]),
    ...mapActions("receipt", [
      "insertReceipt",
      "updateReceipt",
      "getReceiptByID",
    ]),
    ...mapMutations("receipt", ["setReceipts"]),

    addOrUpdateReceipt() {
      const me = this;
      me.modelForm.SuplierID = me.valueSupier;
      me.modelForm.TotalMoney = me.productList.reduce((sum, item) => {
        return sum + (parseInt(item.ReceiptDetail.UnitPrice)* parseInt(item.ReceiptDetail.Quantity));
      }, 0);
      me.modelForm.CreatedBy = JSON.parse(
        sessionStorage.getItem("Account")
      ).UserName;
      const receiptDetail = me.productList.map((item) => {
        item.ReceiptDetail.ProductAttributeID =
          item.ProductAttribute.ProductAttributeID;
        return item.ReceiptDetail;
      });

      const params = {
        Receipt: me.modelForm,
        ReceiptDetails: receiptDetail,
      };
      const h = this.$createElement;
      //gọi api
      if (me.isCreate) {
        me.insertReceipt(params, {
          headers: { "Content-Type": "multipart/form-data" },
        }).then(res=>{
          me.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.InsertSuccess",{object:"đơn nhập hàng"})
                ),
                type: "success",
              });
              me.$router.push("/admin/receipt")
        });
        // me.modelForm={}
        // me.dynamicTags=[]
        // me.valueCategory=null
        // me.valueManufacturer=null
        // me.preview= null
        // me.previewImage= []
      } else {
        const param = {
          id: me.modelForm.ReceiptID,
          params: params,
        };
        me.updateReceipt(param, {
          headers: { "Content-Type": "multipart/form-data" },
        });
      }
    },
    handleClose(tag) {
      this.dynamicTags.splice(this.dynamicTags.indexOf(tag), 1);
    },
    showInput() {
      this.inputVisible = true;
      this.$nextTick((_) => {
        this.$refs.saveTagInput.$refs.input.focus();
      });
    },

    handleInputConfirm() {
      let inputValue = this.inputValue;
      if (inputValue) {
        this.dynamicTags.push(inputValue);
      }
      this.inputVisible = false;
      this.inputValue = "";
    },
    handleChangeWarranty() {},
    openDetailForm() {
      const me = this;
      if (me.dynamicTags.length > 0) {
        me.showDetail = true;
        me.$refs.popup.setModel(true);
      } else {
        me.$alert("Hãy nhập thông tin bộ nhớ", "Cảnh báo", {
          confirmButtonText: "Đồng ý",
        });
      }
    },
    closePopup() {
      const me = this;
      me.showDetail = false;
    },
    updateReceiptAttributes(receiptAttributes) {
      const me = this;
      me.modelForm.ReceiptAttributeList = receiptAttributes;
    },
    selectProduct() {
      const me = this;
      me.$refs.popup.setModel(true);
    },
    setSelectedProducts(selectedProducts) {
      const me = this;
      selectedProducts.forEach(element => {
        element.ReceiptDetail={
          Quantity:null,
          UnitPrice:null,
          Note:null
        }
      });
      me.productList = JSON.parse(JSON.stringify(selectedProducts));
      me.$refs.popup.setModel(false);
    },
    handleDelete(productAttributeId) {
      const me = this;
      me.productList = me.productList.filter((item) => {
        return item.ProductAttribute.ProductAttributeID !== productAttributeId;
      });
      me.$refs.popup.selectedProducts = me.productList;
      me.$refs.popup.indexSelectedProducts = me.productList.map((item) => {
        return item.ProductAttribute.ProductAttributeID;
      });
    },
  },
  watch: {
    dynamicTags() {
      const me = this;
      const t = me.dynamicTags;

      me.modelForm.ReceiptAttributeList = [];
      me.dynamicTags.forEach((item) => {
        const receiptAttribute = me.modelForm.ReceiptAttributes?.filter(
          (ReceiptAttribute) => {
            return (
              ReceiptAttribute.Memory === item ||
              ReceiptAttribute.Memory === parseInt(item)
            );
          }
        );
        if (receiptAttribute && receiptAttribute.length > 0) {
          // const receiptAttribute=me.modelForm.ReceiptAttributes.filter(ReceiptAttribute=>{
          //   return ReceiptAttribute.Memory===item
          // })
          receiptAttribute[0].EndTimeDate = receiptAttribute[0].PromotionEnd;
          receiptAttribute[0].StartTimeDate =
            receiptAttribute[0].PromotionStart;
          me.modelForm.ReceiptAttributeList.push(receiptAttribute[0]);
        } else {
          me.modelForm.ReceiptAttributeList.push({ Memory: parseInt(item) });
        }
      });
    },
  },
};
</script>

<style lang="scss" scoped>
::v-deep {
  .el-form-item__content {
    display: flex;
  }
  .el-input-number {
    width: 100px;
  }
  .el-input-number__decrease,
  .el-input-number__increase {
    width: 20px;
  }
  .el-tag {
    height: 40px;
    line-height: 38px;
  }
  .form-create {
    .form-left{
      .el-form-item__label{
        width:190px !important;
      }
    }
    .form-right {
      .memory {
        .el-form-item__content {
          display: block !important;
          line-height: 50px !important;
        }
      }
    }
  }
}
</style>
<style scoped>
.add-btn {
  @apply tw-bg-red-500 tw-text-white tw-ml-6 !important;
}
.el-tag + .el-tag {
  margin-left: 10px;
}
.button-new-tag {
  height: 40px;
  line-height: 38px;
  padding-top: 0;
  padding-bottom: 0;
}
.input-new-tag {
  width: 90px;
  height: 40px !important;
  line-height: 38px !important;
  margin-left: 10px;
  vertical-align: bottom;
}
</style>
<style lang="scss" scoped>
.receipt-detail-list {
  height: 400px;
}
</style>