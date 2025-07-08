import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import {
  ImageCropperComponent,
  ImageCroppedEvent,
  LoadedImage,
} from 'ngx-image-cropper';
import { CroppedImage } from '../../../core/models/cropped-image';

@Component({
  selector: 'app-image-add',
  imports: [CommonModule, ImageCropperComponent],
  templateUrl: './image-add.component.html',
  styleUrl: './image-add.component.scss',
})
export class ImageAddComponent implements OnInit {
  @Input()
  photo: SafeUrl = '';
  @Output()
  photoChange = new EventEmitter<CroppedImage>();
  imageChangedEvent: Event | null = null;
  @ViewChild('closeModal') modalClose: any;
  croppedImage: SafeUrl = '';
  blob!: Blob; // This will hold the cropped image blob

  constructor(private sanitizer: DomSanitizer) {}

  ngOnInit(): void {}

  fileChangeEvent(event: Event): void {
    this.imageChangedEvent = event;
  }

  imageCropped(event: ImageCroppedEvent) {
    if (event.objectUrl) {
      this.croppedImage = this.sanitizer.bypassSecurityTrustUrl(
        event.objectUrl
      );
      this.blob = event.blob as Blob; // Ensure the blob is set correctly
    }
  }
  imageLoaded(image: LoadedImage) {
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    // show message
  }

  selectionner(): void {
    this.photoChange.emit({
      croppendImage: this.croppedImage,
      blob: this.blob,
    });
    this.modalClose.nativeElement.click();
  }
}
