using Amazon.S3;
using document_service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System;
using System.Threading.Tasks;

namespace document_service.Services
{

    class  CreateBucketTest
    {
       
       
           
        

       
    }

    public class VedioService
    {
        private const string bucketName = "vedios2";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
        private static IAmazonS3 s3Client;


        public int chunkSize;
        private string tempFolder;
        private  IMongoDatabase _database;
        public VedioService(
       IOptions<DocumentDatabaseSettings> documentDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                documentDatabaseSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(
                documentDatabaseSettings.Value.DatabaseName);
            s3Client = new AmazonS3Client("AKIA6MZM6VZYYYUTPC6K", "vUaQlJWw3UzulVDANE7YWFp8bv3V0T4xHRREsDym", bucketRegion);
            //s3Client = new AmazonS3Client(bucketRegion);
        }



        public async Task UploadChunks(string id, string fileName,HttpRequest httpRequest,UploadStatus status)
        {
            try
            {
                var chunkNumber = id;
                string newpath = Path.Combine(tempFolder + "/Temp", fileName + chunkNumber);
                using (FileStream fs = System.IO.File.Create(newpath))
                {
                    byte[] bytes = new byte[chunkSize];
                    int bytesRead = 0;
                    while ((bytesRead = await httpRequest.Body.ReadAsync(bytes, 0, bytes.Length)) > 0)
                    {
                        fs.Write(bytes, 0, bytesRead);
                    }
                 
                }
            }
            catch (Exception ex)
            {

            }


        }

        public async Task CreateAsync(string filepath)
        {
                var bucket = new GridFSBucket(_database);
                var fileBytes = await File.ReadAllBytesAsync(filepath);
                var id = await bucket.UploadFromBytesAsync("filename", fileBytes);

            
        }

    }
}
