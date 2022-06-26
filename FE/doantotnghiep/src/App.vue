<template>

  <router-view />

</template>

<style lang="scss">
// Import Main styles for this application
@import "@/styles/style.scss";
@import url('./assets/css/tailwind.css');
@import url('./assets/css/loader.css');
/* width */
::-webkit-scrollbar {
  width: 8px;
  height:8px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1; 
}
 
/* Handle */
::-webkit-scrollbar-thumb {
  background: rgb(182, 181, 181); 
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555; 
}
</style>
