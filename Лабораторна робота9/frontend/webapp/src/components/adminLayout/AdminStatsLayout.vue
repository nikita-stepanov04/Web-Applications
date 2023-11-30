<template>
  <div class="container">
    <div class="row">
      <h3 class="text-center mb-4">Statistics</h3>
      <div class="col-md-6 col-sm-12" style="height: 400px;">
        <canvas ref="dishPopularity"></canvas>
      </div>
      <div class="col-md-6 col-sm-12" style="height: 400px;">
        <canvas ref="dishTypesPopularity"></canvas>
      </div>
      <div class="col-12 mt-5 fs-5">
        <p><strong>Average dish in cart price: </strong> {{formatCurrency(parseFloat(averageDishInCartPrice))}}</p>
        <p><strong>Average cart price: </strong>{{formatCurrency(parseFloat(averageCartPrice))}}</p>
      </div>
    </div>
  </div>
</template>

<script>
import request from '@/api/requestConstructor'
import helpers from "@/mixins/helpers";
import charts from "@/mixins/charts";

export default {
  mixins: [helpers, request, charts],

  data() {
    return {
      dishPopularity: [],
      dishTypesPopularity: [],
      averageDishInCartPrice: '',
      averageCartPrice: ''
    }
  },
  async created() {
    try {
      this.dishPopularity = (await request.get(
          'stats/dish', {}, true)).data;
      this.dishTypesPopularity = (await request.get(
          'stats/dishtypes', {}, true)).data;
      this.averageDishInCartPrice = (await request.get(
          'stats/dish-in-cart-price', {}, true)).data;
      this.averageCartPrice = (await request.get(
          'stats/cart', {}, true)).data;
      this.createCharts(this.dishPopularity, this.dishTypesPopularity);
    } catch (error) {
      this.alertDanger('Failed to load data');
    }
  }
}
</script>