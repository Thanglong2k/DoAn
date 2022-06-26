<template>
  <div class="crate-product-container">
    <div class="header-area tw-flex tw-justify-start">
      <div class="back tw-w-8 tw-h-8 tw-mx-6 tw-cursor-pointer" @click="onBack">
        <img class="tw-w-full tw-h-full" :src="ic_Back" />
      </div>
      <div class="header-text tw-text-gray-700 tw-text-base tw-font-semibold">
        {{
          isCreate ? $t("Product.AddNewProduct") : $t("Product.EditNewProduct")
        }}
      </div>
    </div>

    <div class="form-create tw-p-6">
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="120px"
        label-position="left"
        class="demo-ruleForm"
      >
        <div class="product-type">
          <el-form-item
            class="tw-flex-1 tw-mr-4 tw-flex"
            :label="$t('Product.ProductType')"
            prop="Type"
          >
            <el-radio-group
              v-model="modelForm.Type"
              :disabled="!isCreate"
              @change="onChangeProductType"
            >
              <el-radio :label="1">Điện thoại</el-radio>
              <el-radio :label="2">Phụ kiện</el-radio>
            </el-radio-group>
          </el-form-item>
          <!-- <el-form-item
            class="tw-flex-1 tw-mr-4 tw-flex"
          >
            <el-button @click="openIMEIList">Danh sách IMEI</el-button>
          </el-form-item> -->
        </div>
        <div class="form-main tw-grid tw-grid-cols-2">
          <div class="form-left tw-col-span-1">
            <el-form-item
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.ProductName')"
              prop="ProductName"
            >
              <el-input
                v-model="modelForm.ProductName"
                :placeholder="$t('Product.ProductNameInput')"
              />
            </el-form-item>
            <div
              class="monitor tw-flex"
              v-if="modelForm.Type === productTypePhone"
            >
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.Monitor')"
                prop="Monitor"
              >
                <el-input
                  v-model="modelForm.Monitor"
                  :placeholder="$t('Product.MonitorInput')"
                />
              </el-form-item>
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.Size')"
                prop="MonitorSize"
              >
                <el-input
                  v-model="modelForm.MonitorSize"
                  :placeholder="$t('Product.Size')"
                />
              </el-form-item>
            </div>

            <div
              class="battery tw-flex"
              v-if="modelForm.Type === productTypePhone"
            >
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.BatteryType')"
                prop="BatteryType"
              >
                <el-input
                  v-model="modelForm.BatteryType"
                  :placeholder="$t('Product.BatteryType')"
                />
              </el-form-item>
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.Battery')"
                prop="Battery"
              >
                <el-input
                  v-model="modelForm.Battery"
                  :placeholder="$t('Product.BatteryInput')"
                />
              </el-form-item>
            </div>

            <el-form-item
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.Camera')"
              prop="Camera"
              v-if="modelForm.Type === productTypePhone"
            >
              <el-input
                v-model="modelForm.Camera"
                :placeholder="$t('Product.CameraInput')"
              />
            </el-form-item>

            <el-form-item
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.Ram')"
              prop="Ram"
              v-if="modelForm.Type === productTypePhone"
            >
              <el-input
                v-model="modelForm.Ram"
                :placeholder="$t('Product.RamInput')"
              />
            </el-form-item>
            <div
              class="weight-size tw-flex"
              v-if="modelForm.Type === productTypePhone"
            >
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.Weight')"
                prop="Weight"
              >
                <el-input
                  v-model="modelForm.Weight"
                  :placeholder="$t('Product.Weight')"
                />
              </el-form-item>
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.Size')"
                prop="Size"
              >
                <el-input
                  v-model="modelForm.Size"
                  :placeholder="$t('Product.Size')"
                />
              </el-form-item>
            </div>
            <div class="origin-release tw-flex">
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.Origin')"
                prop="Origin"
              >
                <el-input
                  v-model="modelForm.Origin"
                  :placeholder="$t('Product.Origin')"
                />
              </el-form-item>
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.ReleaseTime')"
                prop="ReleaseTime"
              >
                <el-date-picker
                  v-model="modelForm.ReleaseTime"
                  type="month"
                  format="MM-yyyy"
                  :placeholder="$t('SelectDate')"
                >
                </el-date-picker>
              </el-form-item>
            </div>
            <div
              class="gpu-os tw-flex"
              v-if="modelForm.Type === productTypePhone"
            >
              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.GPU')"
                prop="GPU"
              >
                <el-input
                  v-model="modelForm.GPU"
                  :placeholder="$t('Product.GPU')"
                />
              </el-form-item>

              <el-form-item
                class="tw-flex-1 tw-mr-4"
                :label="$t('Product.OperatingSystem')"
                prop="OperatingSystem"
              >
                <el-input
                  v-model="modelForm.OperatingSystem"
                  :placeholder="$t('Product.OperatingSystem')"
                />
              </el-form-item>
            </div>
            <div
              class="description tw-flex"
              v-if="modelForm.Type !== productTypePhone"
            >
              <div class="label-item tw-text-sm tw-leading-10">
                Thông tin chi tiết
              </div>
              <div
                class="value-item tw-leading-10 tw-ml-3 tw-cursor-pointer"
                @click="openDesciption"
              >
                Bổ sung thông tin chi tiết
              </div>
            </div>
          </div>
          <div class="form-right tw-col-span-1">
            <el-form-item
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.Manufacturer')"
              prop="ManufacturerID"
            >
              <el-select
                v-model="modelForm.ManufacturerID"
                :placeholder="$t('Product.Manufacturer')"
              >
                <el-option
                  v-for="item in optionManufacturers"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                >
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.Category')"
              prop="CategoryID"
            >
              <el-select
                v-model="modelForm.CategoryID"
                :placeholder="$t('Product.Category')"
              >
                <el-option
                  v-for="item in optionCategories"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                >
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.Avatar')"
            >
              <div class="image-icon tw-flex">
                <div class="image-item tw-relative tw-mr-1">
                  <img width="50" height="50" :src="preview" />
                  <img
                    v-if="avatar !== null"
                    :src="iconRemove"
                    class="tw-absolute tw--top-1 tw-right-0"
                    width="16"
                    height="16"
                    @click="removeAvatar"
                  />
                </div>
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
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.Image')"
            >
              <div class="image-icon tw-flex">
                <div
                  class="image-item tw-relative tw-mr-1"
                  v-for="(item, index) in previewImage"
                  :key="index"
                >
                  <img width="50" height="50" :src="item" />
                  <img
                    :src="iconRemove"
                    class="tw-absolute tw--top-1 tw-right-0"
                    width="16"
                    height="16"
                    @click="removeImage(item)"
                  />
                </div>
              </div>
              <input
                ref="image"
                type="file"
                hidden
                accept=".jpg, .jpeg, .png, .gif, .webp"
                @change="previewImages"
                multiple
              />
              <el-button @click="selectImage">{{
                $t("Button.SelectImage")
              }}</el-button>
            </el-form-item>
            <el-form-item
              class="tw-flex-1 tw-mr-4 memory"
              :label="$t('Product.Memory')"
              prop="Memory"
              v-if="modelForm.Type === productTypePhone"
            >
              <div class="element">
                <el-tag
                  :key="tag"
                  v-for="tag in dynamicTags"
                  closable
                  :disable-transitions="false"
                  @close="handleClose(tag)"
                >
                  {{ tag }}
                </el-tag>
                <el-input
                  class="input-new-tag"
                  v-if="inputVisible"
                  v-model="inputValue"
                  ref="saveTagInput"
                  size="mini"
                  @keyup.enter.native="handleInputConfirm"
                  @blur="handleInputConfirm"
                >
                </el-input>
                <el-button
                  v-else
                  class="button-new-tag"
                  :style="[
                    dynamicTags.length > 0
                      ? { 'margin-left': '10px' }
                      : { 'margin-left': '0px' },
                  ]"
                  size="small"
                  @click="showInput"
                  >{{ "+ " + $t("Product.Memory") }}</el-button
                >
              </div>
              <div class="bonus-info tw-cursor-pointer" @click="openDetailForm">
                Bổ sung thông tin chi tiết
              </div>
            </el-form-item>
            <el-form-item
              class="tw-flex-1 tw-mr-4"
              :label="$t('Product.Warranty')"
              prop="Warranty"
            >
              <el-input-number
                v-model="modelForm.Warranty"
                controls-position="right"
                @change="handleChangeWarranty"
                :min="1"
              ></el-input-number>
              <span class="tw-ml-4">tháng</span>
            </el-form-item>
          </div>
        </div>
      </el-form>
    </div>
    <el-button class="add-btn" @click="addOrUpdateProduct">{{
      isCreate ? $t("Button.Add") : $t("Button.Edit")
    }}</el-button>
    <MoreProductInfo
      ref="popup"
      :productAttributes="productAttributeList"
      v-if="showDetail"
      @closePopup="closePopup"
      @updateProductAttributes="updateProductAttributes"
      @updateError="updateError"
    />
    <Descriptions
      ref="popupDescription"
      :productAttributes="productAttributeList"
      v-if="showDetailDescription"
      @closePopup="closePopupDescription"
      @updateProductAttributes="updateProductAttributes"
    />
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import MoreProductInfo from "./popup/more-product-info-popup.vue";
import Descriptions from "./popup/description-product-popup.vue";
export default {
  components: {
    MoreProductInfo,
    Descriptions,
  },
  data() {
    var checkNumber = (rule, value, callback) => {
      if (value) {
        if (!Number(value)) {
          callback(new Error("Hãy nhập số"));
        }else{
          callback();
        }
      }else{
          callback();
        }
    };
    var checkNumberInt = (rule, value, callback) => {
      if (value) {
        if (!Number.isInteger(Number(value))) {
          callback(new Error("Hãy nhập số nguyên"));
        }else{
          callback();
        }
      }else{
          callback();
        }
    };
    return {
      modelForm: {
        Type: 1,
      },
      valueManufacturer: "",
      valueCategory: "",
      rules: {
        ProductName: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        ManufacturerID: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        CategoryID: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        Origin: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        ReleaseTime: [
          { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        ],
        Ram: [{ validator:checkNumberInt, message: "Phải nhập số nguyên!", trigger: "blur" }],
        Weight: [{ validator:checkNumberInt, message: "Phải nhập số nguyên!", trigger: "blur" }],
        Battery: [{ validator:checkNumberInt, message: "Phải nhập số nguyên!", trigger: "blur" }],
        MonitorSize: [
          {
            validator: checkNumber,
            message: "Phải nhập số!",
            trigger: "blur",
          },
        ],

        // Warranty:[
        //   { required: true, message: "Hãy nhập thông tin!", trigger: "blur" },
        // ],
      },
      preview: null,
      previewImage: [],
      avatar: null,
      image: [],
      dynamicTags: [],
      inputVisible: false,
      inputValue: "",
      showDetail: false,
      showDetailDescription: false,
      iconRemove: require("@/assets/icons/product-detail/remove.png"),
      ic_Back: require("@/assets/icons/header/back.png"),
      productTypePhone: this.$enumeration.ProductType.Phone,
      productTypeAccessory: this.$enumeration.ProductType.Accessory,
      error: false,
      errorInfo:false,
      productAttributeList:[]
    };
  },
  computed: {
    ...mapState("manufacturer", ["manufacturers"]),
    ...mapState("category", ["categoryAll"]),
    ...mapState("product", ["product"]),
    /**
     * Giá trị option chọn hãng sản xuất
     */
    optionManufacturers() {
      const me = this;
      return me.manufacturers?.map((item) => {
        return { value: item.ManufacturerID, label: item.ManufacturerName };
      });
    },
    /**
     * Giá trị option chọn hãng sản xuất
     */
    optionCategories() {
      const me = this;
      return me.categoryAll?.map((item) => {
        return { value: item.CategoryID, label: item.CategoryName };
      });
    },
    isCreate() {
      const me = this;
      return me.$route.query.create ? true : false;
    },
  },
  async created() {
    const me = this;
    await me.getManufacturers();
    await me.getCategories();
    me.modelForm.Warranty = 12;
    //nếu là update thì lấy data
    if (!me.isCreate) {
      await me.getProductByID(me.$route.query.productID);
      if (me.product) {
        me.modelForm = JSON.parse(JSON.stringify(me.product));
        me.modelForm.ProductAttributeList = me.modelForm.ProductAttributes;
        me.productAttributeList=JSON.parse(JSON.stringify(me.modelForm.ProductAttributeList))
        me.dynamicTags = me.modelForm.ProductAttributes.map((item) => {
          return item.Memory;
        });
        me.preview = me.modelForm.Avatar;
        const previewImage = me.modelForm.Image.split(",");
        me.previewImage = previewImage;
        me.valueCategory = me.modelForm.CategoryID;
        me.valueManufacturer = me.modelForm.ManufacturerID;
      }
    }
  },
  methods: {
    ...mapActions("manufacturer", ["getManufacturers"]),
    ...mapActions("category", ["getCategories"]),
    ...mapActions("customer", ["getCustomerByPhoneNumber"]),
    ...mapActions("product", [
      "insertProduct",
      "updateProduct",
      "getProductByID",
    ]),
    onBack() {
      const me = this;
      me.$router.push("/admin/product");
    },
    selectAvatar() {
      this.$refs.avatar.click();
    },
    selectImage() {
      this.$refs.image.click();
    },
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
    previewImages(event) {
      var input = event.target.files;
      console.log(input);

      if (input) {
        if (this.modelForm.Image && !Array.isArray(this.modelForm.Image)) {
          this.modelForm.Image = this.modelForm.Image.split(",");
        }
        for (let i = 0; i < input.length; i++) {
          let reader = new FileReader();
          reader.onload = (e) => {
            this.previewImage.push(e.target.result);
          };
          this.image.push(input[i]);
          reader.readAsDataURL(input[i]);
        }
      }
    },
    addOrUpdateProduct() {
      const me = this;
      const error=me.error
      if(!me.modelForm.ProductAttributeList||me.modelForm.ProductAttributeList.length===0){
        me.errorInfo=true
        
      }else{
        me.errorInfo=false
      }
      // me.modelForm.CategoryID = me.valueCategory;
      // me.modelForm.ManufacturerID = me.valueManufacturer;
      me.$refs["ruleForm"].validate(async (valid) => {
        if (valid && !me.error && !me.errorInfo) {
          if (me.modelForm.Type === me.productTypePhone) {
            if (me.dynamicTags.length === 0) {
              const h = this.$createElement;
              me.$notify.error({
                title: me.$t("Notify.Error"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  "Hãy nhập thông tin dung lượng"
                ),
              });
              return;
            }
          }
          //gán file ảnh

          if (me.image.length > 0) {
            me.modelForm.ImageFile = me.image;
          }
          if (me.avatar !== null) {
            me.modelForm.AvatarFile = me.avatar;
          }
          me.modelForm.ReleaseTime=me.$commonFn.addHours(7,new Date(me.modelForm.ReleaseTime))
          let formData = new FormData();
          for (var item in me.modelForm) {
            if (me.modelForm[item] !== null) {
              if (
                (Array.isArray(me.modelForm[item]) ||
                  typeof me.modelForm[item] === "object") &&
                item !== "ImageFile" &&
                item !== "AvatarFile" &&
                item !== "Image"
              ) {
                me.modelForm[item] = JSON.stringify(me.modelForm[item]);
              }
              if (item === "ImageFile") {
                for (let imageFile in me.modelForm[item]) {
                  formData.append(
                    item,
                    me.modelForm[item][imageFile],
                    me.modelForm[item][imageFile].name
                  );
                }
              } else {
                formData.append(item, me.modelForm[item]);
              }
            }
          }
          const h = this.$createElement;
          //gọi api
          if (me.isCreate) {
            me.insertProduct(formData, {
              headers: { "Content-Type": "multipart/form-data" },
            })
              .then((res) => {
                me.$notify({
                  title: me.$t("Notify.Success"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.InsertProductSuccess")
                  ),
                  type: "success",
                });
                me.modelForm = { Type: me.productTypePhone };
                me.dynamicTags = [];
                me.valueCategory = null;
                me.valueManufacturer = null;
                me.preview = null;
                me.image = [];
                me.avatar = null;
                me.previewImage = [];
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
          } else {
            const param = {
              id: me.modelForm.ProductID,
              formData: formData,
            };

            me.updateProduct(param, {
              headers: { "Content-Type": "multipart/form-data" },
            })
              .then((res) => {
                me.$notify({
                  title: me.$t("Notify.Success"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.UpdateProductSuccess")
                  ),
                  type: "success",
                });
                me.$router.push("/admin/product");
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
        } else {
          if(me.errorInfo){
            const h = this.$createElement;
        me.$notify.error({
                title: me.$t("Notify.Error"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  "Hãy nhập thông tin chi tiết"
                ),
              });
          }
          else if (me.error) {
            const h = this.$createElement;
            me.$notify.error({
              title: me.$t("Notify.Error"),
              message: h(
                "i",
                { style: "color: teal" },
                "Thông tin sai. Hãy kiểm tra lại!"
              ),
            });
          }
        }
      });
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
        me.$nextTick((_) => {
          me.$refs.popup.setModel(true);
        });
      } else {
        me.$alert("Hãy nhập thông tin bộ nhớ", "Cảnh báo", {
          confirmButtonText: "Đồng ý",
        });
      }
    },
    openDesciption() {
      const me = this;
      me.showDetailDescription = true;
      me.$nextTick((_) => {
        me.$refs.popupDescription.setModel(true);
      });
    },
    closePopup() {
      const me = this;
      me.showDetail = false;
    },
    closePopupDescription() {
      const me = this;
      me.showDetailDescription = false;
    },
    updateProductAttributes(productAttributes) {
      const me = this;
      debugger
      productAttributes=productAttributes.map(item=>{
        item.RangeDate={}
        if(item.StartTimeDate){
          //this.modelForm[index].RangeDate.StartDate=new Date()
          item.RangeDate.StartDate=this.$commonFn.addHours(7,new Date(item.StartTimeDate))
        }
        if(item.EndTimeDate){
          //this.modelForm[index].RangeDate.EndDate=new Date()
          item.RangeDate.EndDate=this.$commonFn.addHours(7,new Date(item.EndTimeDate))
        }
        if(!item.StartTimeDate||new Date(item.StartTimeDate).toISOString()===new Date('1970-01-01T07:00:00.000Z').toISOString()||!item.EndTimeDate||new Date(item.EndTimeDate).toISOString()===new Date('1970-01-01T07:00:00.000Z').toISOString()){
          item.RangeDate=null
        }
        return item
      })
      
      me.modelForm.ProductAttributeList = productAttributes;
      me.modelForm.ProductAttributes = productAttributes;
      me.productAttributeList=JSON.parse(JSON.stringify(me.modelForm.ProductAttributeList))
    },
    updateError(val) {
      const me = this;
      me.error = val;
    },
    removeAvatar() {
      const me = this;
      me.preview = null;
      me.avatar = null;
      me.modelForm.AvatarFile = null;
    },
    removeImage(image) {
      const me = this;
      const indexPreviewImage = me.previewImage.indexOf(image);
      me.previewImage.splice(indexPreviewImage, 1);
      me.modelForm.Image = me.previewImage;
      const indexImage = me.image.indexOf(image);
      me.image.splice(indexImage, 1);
      me.modelForm.ImageFile = me.image;
    },
    openIMEIList() {
      const me = this;
      me.$router.push({
        path: "/admin/product/imei",
        query: {
          productID: me.product.ProductID,
        },
      });
    },
    onChangeProductType() {
      const me = this;
    },
  },
  watch: {
    dynamicTags() {
      const me = this;
      const t = me.dynamicTags;
      me.modelForm.ProductAttributeList = [];
      me.productAttributeList=[]
      debugger
      let productAttributes=[]
      if(me.modelForm.ProductAttributes){
        productAttributes=[...me.modelForm.ProductAttributes]
      }
      
      me.modelForm.ProductAttributes=[]
      //sau khi thay đổi dung lượng thì gán lại giá trị cho ProductAttributes và productAttributeList
      me.dynamicTags.forEach((item) => {
        debugger
        const productAttribute = productAttributes?.filter(
          (ProductAttribute) => {
            return (
              ProductAttribute.Memory === item ||
              ProductAttribute.Memory === parseInt(item)
            );
          }
        );
        if (productAttribute && productAttribute.length > 0) {
          productAttribute[0].EndTimeDate = productAttribute[0].PromotionEnd?productAttribute[0].PromotionEnd:productAttribute[0].EndTimeDate;
          productAttribute[0].StartTimeDate = productAttribute[0].PromotionStart?productAttribute[0].PromotionStart:productAttribute[0].StartTimeDate;
          me.modelForm.ProductAttributeList.push(productAttribute[0]);
          me.modelForm.ProductAttributes.push(productAttribute[0]);
          me.productAttributeList.push(productAttribute[0])
        } else {
          me.modelForm.ProductAttributeList.push({ Memory: parseInt(item) });
          me.modelForm.ProductAttributes.push({ Memory: parseInt(item) });
          me.productAttributeList.push({ Memory: parseInt(item) });
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
    .product-type {
      .el-form-item__content {
        display: inline !important;
        line-height: 50px !important;
        margin-left: 0 !important;
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