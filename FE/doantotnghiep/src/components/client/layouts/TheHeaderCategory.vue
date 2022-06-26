<template>
  <div class="category-container tw-h-12 tw-bg-yellow-400 tw-w-full">
    <div
      class="
        category
        tw-bg-transparent
        tw-flex
        tw-justify-between
        tw-items-center
        tw-h-full
        tw-w-3/4
        tw-mx-auto
        tw-my-0
      "
    >
      <router-link
        class="
          category-item
          tw-flex
          tw-justify-center
          tw-items-center
          tw-no-underline
          tw-font-bold
          tw-min-w-min
          tw-h-full
          tw-cursor-pointer
        "
        :to="{
          name: 'ListProduct',
          params: { category: item.Meta },
          query: {
            categoryID: item.CategoryID,
          },
        }"
        :class="{ 'tw-px-3': !isChildren(index) }"
        v-for="(item, index) in categories"
        :key="item.CategoryID"
      >
        <div
          class="item-category tw-h-full tw-flex tw-items-center"
          v-if="!isChildren(index)"
        >
          <img
            v-if="item.Image"
            style="width: 80px; height: 40px"
            :src="item.Image"
          />
          <span v-else>{{ item.CategoryName }}</span>
        </div>
        <v-menu v-else offset-y open-on-hover rounded="0">
          <template v-slot:activator="{ on, attrs }">
            <div
              class="
                item-category
                tw-h-full tw-w-full tw-flex tw-items-center tw-px-3
              "
              v-bind="attrs"
              v-on="on"
            >
              <div>
                {{ item.CategoryName }}
              </div>
              <img
                class="tw-ml-2"
                :src="icDownArrow"
                style="width: 14px; height: 14px"
              />
            </div>
          </template>
          <v-list>
            <v-list-item
              v-for="child in item.Children"
              :key="child.CategoryID"
              
            >
              <router-link
              class="tw-no-underline"
                :to="{
                  name: 'ListProduct',
                  params: { category: child.Meta },
                  query: {
                    categoryID: child.CategoryID,
                    accessory:true
                  },
                }"
              >
                <img
                  v-if="child.Image !== null"
                  style="width: 80px; height: 40px"
                  :src="child.Image"
                />
                <v-list-item-title>{{ child.CategoryName }}</v-list-item-title>
              </router-link>
            </v-list-item>
          </v-list>
        </v-menu>
        <!-- <div v-else class="dropdown-category tw-flex tw-items-center">
          <img
            v-if="item.Image"
            style="width: 80px; height: 40px"
            :src="item.Image"
          />
          <span v-else>{{ item.CategoryName }}</span>
          <img :src="icDownArrow" style="width: 16px; height: 16px" />
        </div> -->
        <!-- <div v-if="item.Children!==undefined" class="item-category-children">
            <div class="item" v-for="(child,index) in item.Children" :key="index">
              <img v-if="child.Image" style="width:80px; height:40px;" :src="child.Image"/>
              <span v-else>{{child.CategoryName}}</span>
            </div>
          </div> -->
      </router-link>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
//import { CDropdown,CDropdownToggle,CDropdownMenu ,CDropdownItem,CDropdownDivider} from '@coreui/vue';
export default {
  components: {
    //CDropdown,CDropdownToggle,CDropdownMenu ,CDropdownItem,CDropdownDivider
  },
  data() {
    return {
      icDownArrow: require("@/assets/icons/header/down-arrow.png"),
    };
  },
  async created() {
    const me = this;
    await me.getCategories();
    const t = me.categories;
    if (!t[0].Children) {
      const d = t;
    }
  },
  computed: {
    ...mapState("category", ["categories"]),
  },
  methods: {
    ...mapActions("category", ["getCategories"]),
    ...mapActions("product", ["getProductsByCategory"]),
    /**
     * Chọn danh mục sản phẩm
     */
    async onClickCategory(categoryID) {
      const me = this;
      const params = {
        categoryID: categoryID,
        queryText:'',
        filter:[],
        pageSize: 1,
        pageIndex: 1,
        sortBy: 0,
      };
      await me.getProductsByCategory(params);
      // me.$router.push({
      //   name: "ListProduct",
      //   params: { category: category.Meta },
      //   query: {
      //     categoryID: category.CategoryID,
      //   },
      // });
    },
    isChildren(index) {
      const me = this;
      if (me.categories[index].Children) {
        return true;
      }
      return false;
    },
    on() {
      alert("à");
    },
  },
};
</script>
<style scoped>
.category-item{
  @apply tw-text-black !important;
}
</style>
<style lang="scss" scoped>
.category-container {
  .category {
    &-item {
      &:hover {
        background-color: white;
      }
    }
  }
}
.v-list {
  padding: 0;
}
.v-list-item {
  cursor: pointer;
}
.router-link-exact-active.router-link-active{
  background-color: rgb(245, 240, 240);
  color:black;
}
</style>