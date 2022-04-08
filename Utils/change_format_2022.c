#include <stdio.h>
#include <stdlib.h>

#define SIZE 160353104

struct pixel {
	long long ts;
	short x;
	short y;
	char col;
}__attribute__((packed));

struct pixel_short {
	short x;
	short y;
	char col;
}__attribute__((packed));

struct pixel data[SIZE];
struct pixel_short data_short[SIZE];

int cmp ( const void * first, const void * second ) {
	return ( (*(struct pixel *)first).ts - (*(struct pixel *)second).ts );
}

int main(int argc, char const *argv[])
{
	FILE *ptr = fopen("data_2022_not_sorted_little.bin","rb"); 
	FILE *ptrwrite = fopen("data.bin","wb");
	
	fread(data,sizeof(struct pixel),SIZE,ptr);
	qsort(data,SIZE,sizeof(struct pixel),cmp);
	for(int i = 0; i < SIZE; i++){
		data_short[i].x = data[i].x;
		data_short[i].y = data[i].y;
		data_short[i].col = data[i].col;
	} 
	fwrite(data_short,sizeof(struct pixel_short),SIZE,ptrwrite); 

	return 0;
}