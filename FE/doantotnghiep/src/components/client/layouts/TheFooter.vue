<template>
<div class="footer-container tw-mt-4  tw-w-full tw-bg-red-500">

  <div class="footer-area  tw-grid tw-grid-cols-3 tw-gap-2 tw-h-full tw-w-4/5 tw-bg-transparent tw-mx-auto tw-my-0">
    <div class="footer_item tw-h-full tw-w-full">
      <div
        class="footer_item-title tw-pt-2 tw-pb-2 tw-text-yellow-400 tw-font-bold tw-text-xl"
      >
        Hệ thống cửa hàng
      </div>
      <div
        class="footer_item-content tw-text-sm tw-text-white"
        v-for="(item, index) in store"
        :key="index"
      >
        <p>Địa chỉ: {{ item.Address }}</p>
        <p>Thời gian: {{ item.WorkSchedule }}</p>
      </div>
    </div>
    <div class="footer_item tw-h-full tw-w-full">
      <div
        class="footer_item-title tw-pt-2 tw-pb-2 tw-text-yellow-400 tw-font-bold tw-text-xl"
      >
        Hỗ trợ khách hàng
      </div>
      <div class="footer_item-content tw-text-sm tw-text-white">
        <p>Hỗ trợ mua hàng trực tuyến</p>
        <p>Các hình thức thanh toán</p>
        <p>Chính sách bảo hành</p>
      </div>
    </div>
    <div class="footer_item tw-h-full tw-w-full">
      <div
        class="footer_item-title tw-pt-2 tw-pb-2 tw-text-yellow-400 tw-font-bold tw-text-xl"
      >
        Liên hệ
      </div>
      <div class="footer_item-content tw-text-sm tw-text-white">
        <p>Phone: 0965518362</p>
        <p>Email: thanglong2k@gmail.com</p>
        <p>Gọi mua hàng: 1800.2097 (8h00 - 22h00)</p>
<p>Gọi khiếu nại: 1800.2063 (8h00 - 21h30)</p>

<p>Gọi bảo hành: 1800.2064 (8h00 - 21h00)</p>
      </div>
    </div>
  </div>
</div>
</template>

<script>
export default {
  name: "Footer",
  data() {
    return {
      store: [
        { Address: "74-Thái Hà, Đống Đa, Hà Nội", WorkSchedule: "08:00-21:00" },
      ],
    };
  },
};
</script>

<style lang="scss" scoped>
.footer-area {
  &_item {
    &-title {
    }
    &-content {
      p {
        padding: 4px 0;
      }
    }
  }
}
</style>