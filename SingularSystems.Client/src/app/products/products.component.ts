import { Component } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataService } from '../data.service';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent {
  products: any[] = [];
  page: number = 1;
  pageSize: number = 4; // Default page size
  collectionSize: number = 0;

	constructor(private dataService: DataService) {
	}

  ngOnInit() {
    this.refreshProducts();
}


  refreshProducts() {
    this.dataService.getProducts().subscribe((result) => {
        this.collectionSize = result.length;
        this.products = result.map((product: any, i: number) => ({ id: i + 1, ...product })).slice(
            (this.page - 1) * this.pageSize,
            (this.page - 1) * this.pageSize + this.pageSize,
        );
    });
}

trackByProductId(index: number, product: any): number {
    return product.id;
}
}
