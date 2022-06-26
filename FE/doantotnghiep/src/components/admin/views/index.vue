<template>
  <div class="text-yellow-900">
    <line-chart :chartData="dataLines" :options="options"></line-chart>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dataLines: {
        labels: [1975, 1980, 1985, 1990, 1995, 2000, 2005, 2010, 2015, 2020],
        datasets:[
          {
            data: [70, 95, 100, 120, 257, 271, 300, 321, 383, 450],
            label: "Cây lương thực",
            borderColor: "#3e95cd",
          },
          {
            data: [70, 80, 111, 129, 135, 209, 247, 372, 400, 426],
            label: "Cây công nghiệp",
            borderColor: "#8e5ea2",
          },
          {
            data: [70, 78, 128, 150, 203, 276, 300, 317, 375, 434],
            label: "Rau đậu",
            borderColor: "#3cba9f",
          },
          {
            data: [70, 107, 170, 200, 254, 293, 314, 337, 268, 384],
            label: "Cây ăn quả",
            borderColor: "#e8c3b9",
          },
          {
            data: [70, 100, 135, 157, 187, 201, 222, 272, 312, 433],
            label: "Cây khác",
            borderColor: "#c45850",
          },
        ]
      },
      options: {
        title: {
          display: true,
          text: "Đồ thị tăng trưởng giá trị sản xuất các nhóm cây trồng",
        },
        responsive: true,
        maintainAspectRatio: false,
      },
    };
  },
};
</script>

<style>
</style>