import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IProductBrand } from '../shared/models/productBrand';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search', { static: false }) searchTerm?: ElementRef

  products?: IProduct[];
  productBrands?: IProductBrand[]
  productTypes?: IProductType[]
  shopParams = new ShopParams();
  totalCount?: number;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to Hight', value: 'priceAsc' },
    { name: 'Price: Hight to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) {
  }

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        this.products = response?.data;
        this.shopParams.pageNumber = response?.pageIndex!;
        this.shopParams.pageSize = response?.pageSize!;
        this.totalCount = response?.count;
      },
      error: (error) => { console.log(error) }
    });
  }

  getProductBrands() {
    this.shopService.getProductBrands().subscribe({
      next: (response) => { this.productBrands = [{ id: 0, name: 'All' }, ...response]; },
      error: (error) => { console.log(error) }
    });
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe({
      next: (response) => { this.productTypes = [{ id: 0, name: 'All' }, ...response]; },
      error: (error) => { console.log(error) }
    });
  }

  onProductBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onProductTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(event: Event) {
    let sort = (event.target as HTMLInputElement).value
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts()
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm!.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
