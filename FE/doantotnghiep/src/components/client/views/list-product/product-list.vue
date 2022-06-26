<template>
  <div class="list-product tw-w-4/5">
    <div
      class="
        list-product-header
        tw-mb-3 tw-p-4 tw-bg-white tw-flex tw-justify-between
      "
    >
      <div class="header-left tw-flex-1">
        <span class="tw-text-gray-700 tw-font-semibold tw-text-2xl"></span>
        ({{ productTotal }} sản phẩm)
      </div>
      <div class="header-right tw-w-32">
        <el-select v-model="valueSort" placeholder="Select">
          <el-option
            v-for="item in options"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          >
          </el-option>
        </el-select>
      </div>
    </div>
    <div class="list-produce-body tw-bg-white" v-if="products.length>0">
      <ProductArea title="" :grid="true" :columnNumber="4">
        <ProductItem
          v-for="item in products"
          :key="item.ProductID"
          :dataItem="item"
        />
      </ProductArea>
      <v-pagination
        v-model="pageIndex"
        :length="pageTotal"
        :total-visible="7"
      ></v-pagination>
    </div>
    <data-empty v-else :text="$t('DataEmpty.SearchFilter')"/>
  </div>
</template>

<script>
import ProductItem from "@/components/controls/item-card";
import ProductArea from "@/components/controls/area-card";
import DataEmpty from "@/components/client/views/data-empty";
import { mapState, mapActions } from "vuex";
export default {
  components: {
    ProductArea,
    ProductItem,
    DataEmpty
  },
  data() {
    return {
      pageIndex: 1,
      options: [
        {
          value: 0,
          label: "Giá giảm",
        },
        {
          value: 1,
          label: "Giá tăng",
        },
      ],
      valueSort: 1,
    };
  },
  computed: {
    ...mapState("product", ["products", "pageTotal", "productTotal", "filter"]),
  },
  mounted() {
    const me = this;
    //nếu load lại trang thì sẽ get lại dữ liệu
    // if (me.products.length === 0) {
      me.$commonFn.showMark("list-product")
      me.pageIndex = 1;
      me.getProducts(me.pageIndex);
      me.$commonFn.hideMark("list-product")
    //}
  },

  methods: {
    ...mapActions("product", ["getProductsByCategory"]),
    async getProducts(pageIndex) {
      const me = this;
      const queryText = sessionStorage.getItem("QUERYTEXT");

      const params = {
        categoryID: me.$route.query.categoryID
          ? me.$route.query.categoryID
          : "",
        queryText: queryText,
        filter: me.filter !== null ? me.filter : [],
        pageSize: 10,
        pageIndex: pageIndex,
        sortBy: me.valueSort,
      };
      await me.getProductsByCategory(params);
    },
  },
  watch: {
    pageIndex() {
      const me = this;
      me.getProducts(me.pageIndex);
    },
    valueSort(){
      const me = this;
      me.getProducts(me.pageIndex);
    }
  },
};
</script>

<style>
</style>