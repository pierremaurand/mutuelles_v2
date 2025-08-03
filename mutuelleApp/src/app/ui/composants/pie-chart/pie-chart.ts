import { Component, Input } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-pie-chart',
  imports: [BaseChartDirective],
  templateUrl: './pie-chart.html',
  styleUrl: './pie-chart.scss',
})
export class PieChart {
  @Input() chartData: any;
}
