import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  baseUrl: string = environment.baseUrl + '/fileUpload';
  constructor(private http: HttpClient) {}

  uploadFile(theBlob: Blob): Observable<string> {
    const file = this.blobToFile(theBlob, 'image.png'); // Convert Blob to File with a name
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<string>(`${this.baseUrl}/upload`, formData);
  }

  private blobToFile = (theBlob: Blob, fileName: string): File => {
    return new File(
      [theBlob as any], // cast as any
      fileName,
      {
        lastModified: new Date().getTime(),
        type: theBlob.type,
      }
    );
  };
}
