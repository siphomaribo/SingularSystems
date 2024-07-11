import { Component, ViewChild } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataService } from '../data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


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
  summaryData: any;
  selectedImageUrl: string = '';
  selectedImageDescription: string = '';
  
  @ViewChild('summaryModal', { static: true }) summaryModal: any;
  @ViewChild('imageModal', { static: true }) imageModal: any;

	constructor(private dataService: DataService,  private modalService: NgbModal) {
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

openSummaryModal(productId: any) {
  this.dataService.getProductSummary(productId).subscribe((data) => {
      this.summaryData = data;
      this.modalService.open(this.summaryModal);
  });
}

openImageModal(imageUrl: string, description: string) {
  this.selectedImageUrl = imageUrl;
  this.selectedImageDescription = description;
  this.modalService.open(this.imageModal);
}

}
