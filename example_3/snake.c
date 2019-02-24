#include <stdio.h>
#include <stdlib.h>
int n,m,x,y,x0,y0,rep1,rep2,rep3,rep4,t;
int z=0;
int k=0;
void zukl(int* a );
void down(int st1, int* a);
void dia2(int st1, int* a);
void up(int st1, int* a);
void dia1(int st1, int* a);
void output1(int i,int j, int*a);
void output2(int i, int* a);

void zukl(int* a  ){
   rep4=1;
	dia1(1,a);
//	scanf("%i",&t);
	rep1=1;
	down(1+4*k,a);
	rep2=1;
	dia2(2+4*k,a);
	rep3=1;
	up(2+4*k,a);
	rep4=1;
	dia1(2+4*k,a);
	rep3=1;
	up(1,a);
	rep2=1;
	dia2(3+4*k,a);
	rep1=1;
	down(4+4*k,a);
	rep4=1;
	dia1(4+4*k,a);
	rep3=1;
	up(4+4*k,a);
	if (k>=((2*m*n)/10)+100) return;
	else{
	k++;
	zukl(a); return;}
}
void down(int st1, int* a){
	if (x0>=0&&x0<n&&y0>0&&y0<=m){
		z++; *(a+x0*m+y0-1)=z;
		}
	else z++;
	x0++;
	if (rep1==st1) return;
	else{
	rep1++;
	down(st1,a);
	return;}
}
void dia2(int st1, int* a){
	if (x0>=0&&x0<n&&y0>1&&y0<=m+1){
		z++; *(a+x0*m+y0-2)=z;
		}
	else z++;
	x0++;
	y0--;
	if (rep2==st1) return;
	else{
	rep2++;
	dia2(st1,a);
	return;}
}
void up(int st1, int* a){
	if (x0>1&&x0<=n+1&&y0>0&&y0<=m){
		z++; *(a+(x0-2)*m+y0-1)=z;
		}
	else z++;
	x0--;
	if (rep3==st1) return;
	else{
	rep3++;
	up(st1,a);
	return;}
}
void dia1(int st1, int*a){
	if (x0>1&&x0<=n+1&&y0>=0&&y0<m){
		z++; *(a+(x0-2)*m+y0)=z;
		}
		else z++;
	x0--;
	y0++;
	if (rep4==st1) return;
	else{
	rep4++;
	dia1(st1,a);
	return;}
}
void output1(int i,int j, int*a){
	printf("%5d ",*(a+i*m+j));
	if (j!=m-1) j++;
	else {
		printf("\n"); return;
	}
	output1(i,j,a);
	return;
}
void output2(int i, int* a){
	 int j=0;
	 output1(i,j, a);
	 if (i!=n-1) i++;
	 else return;
	 output2(i,a);
	 return;
}
int main(){
	printf("n,m,x,y: \n");
	fflush (stdin);
	if (scanf("%i", &n)==1 && n>0 && n<14&&scanf("%i", &m)==1 && m>0 && m<14&&scanf("%i", &x)==1 &&  x<=50&&scanf("%i", &y)==1  && y<=50){
		int *a = (int *)malloc(n*m*sizeof(int));
		*(a+(x-1)*m+y-1)=z;
		 x0=x; y0=y;
		zukl(a);
		printf("\n");
		int i=0;
		output2(i,a);
		return 0;

	}
	else puts("Error");

}

