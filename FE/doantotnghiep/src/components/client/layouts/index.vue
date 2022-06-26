<template>
  <v-app>
    <the-header-top />
    <the-header-category />
    <the-main/>
    <the-footer />
  </v-app>
</template>

<script>
import TheFooter from "./TheFooter.vue";
import TheHeaderTop from "./TheHeaderTop.vue";
import TheHeaderCategory from "./TheHeaderCategory.vue";
import TheMain from "./TheMain.vue";
export default {
  components: { TheFooter, TheHeaderTop, TheHeaderCategory, TheMain },
};
</script>

<style lang="scss">
.v-application{
  background-image: url('../../../assets/images/clients/backgrounds/bg_tgdd.png') !important;
      background-size: cover !important;
    background-attachment: fixed !important;
    overflow-x: hidden;
}
</style>