<template>
  <v-card class="area-card tw-border-none">
    <div class="header"
    :class="[backgroundColor,textColor]"
    >
      <v-card-title>{{ title }}</v-card-title>
      <div class="tw-text-xs tw-text-blue-600 tw-cursor-pointer"
        v-if="showReviewAll"
        @click="reviewAll"
      >Xem tất cả</div>
    </div>
    <div class="body tw-p-2"
      :class="[{'tw-grid tw-gap-4':grid},`${gridColumn}`]"
    >
      <slot/>
    </div>
    <!-- <div class="view-more tw-flex tw-justify-center tw-pb-3">
      <slot name="more"/>
    </div> -->
  </v-card>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";
export default {
  
  props: {
    title: {
      type: String,
      default: "Title",
    },
    grid: {
      type: Boolean,
      default: false,
    },
    showReviewAll:{
        type:Boolean,
        default:false
    },
    backgroundColor: {
      type: String,
      default: "tw-bg-white",
    },
    textColor: {
      type: String,
      default: "tw-bg-black",
    },
    columnNumber:{
      type:Number,
      default:4
    },
    type:{
      type:Number,
      default:0
    }
  },
  computed:{
    gridColumn(){
      console.log(this.columnNumber)
      return 'tw-grid-cols-'+this.columnNumber
    }
  },
  methods:{
...mapActions("product", ["getProductsByCategory"]),
      /**
     * Tìm kiếm sản phẩm
     */
    async reviewAll(){
      const me = this
      if(me.type===me.$enumeration.AreaCardType.Product){

        const params = {
          categoryID: '',
          queryText:me.querySearch,
          filter:[],
          pageSize: 10,
          pageIndex: 1,
          sortBy: 0,
        };
        await me.getProductsByCategory(params);
        if(me.$route.fullPath!=='/search'){
          me.$router.push('/search')
        }
      }else{    
        me.$emit("viewAllPost")
      }
      
    }
  }
};
</script>

<style lang="scss" scoped>
.area-card {
  border-radius: 8px;
  .header {
    border: none;
    .v-card__title {
      padding: 0 !important;
    }
  }
}
</style>
<style >
.header .v-card__title {
  padding: 0 !important;
  font-size:22px !important;
  @apply tw-font-bold !important;
}
</style>