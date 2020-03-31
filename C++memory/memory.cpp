/*
    20171222185
    qq:1439698007
    evosimy.com
 */

#include <ctime>
#include <iostream>
#include <cstdlib>
#include <cstdio>

using namespace std;
const int num = 10; //生成储存块的数量
//储存块
struct pro_block
{
	int block_num;
	int size_KB;
	int origin;
	int end;
	int state;
	pro_block *next;
};
//进程
struct Process
{
    int num = -1;
    int sz_kb;
    pro_block *memory_pro;
    Process *next;
};
//输出已经分配的储存块
void put_used(pro_block *&first)
{
    pro_block *temp = first;
    cout << "------------------------------------------------------------------------------" << endl;
	cout << "\t分区号\t大小(KB)\t起址(K)\t终址(K)\t状态" << endl;
    while(temp != NULL)
    {
        if(temp->state == 1)
            cout << "\t " << temp->block_num << "\t " << temp->size_KB << "\t\t " << temp->origin << "\t " << temp->end << "\t " << temp->state << endl;
            temp = temp->next;
    }
}
//扫描储存块链表，合并相邻的空闲储存块
void scan(pro_block *&first)
{
    pro_block *temp = first;
    while(temp->next != NULL)
    {
        if(temp->end == temp->next->origin && temp->next->state == 0 && temp->next->state == 0)
        {
            temp->size_KB += temp->next->size_KB;
            temp->end = temp->next->end;
            pro_block *t = temp->next;
            temp->next = temp->next->next;
            delete t;
            t = temp->next;
            while(t != NULL)
            {
                t->block_num--;
                t = t->next;
            }
        }
        temp = temp->next;
    }
}
//释放储存块
void free_pro(int i, pro_block *&first)
{
    pro_block *temp = first;
    while(--i) temp = temp->next;
    temp->state = 0;
    scan(first);
}
//输出分区说明表
void put_table(pro_block *&first)
{
	cout << "------------------------------------------------------------------------------" << endl;
	cout << "\t分区号\t大小(KB)\t起址(K)\t终址(K)\t状态" << endl;
	pro_block *temp = first;
	while (temp != NULL)
	{
		cout << "\t " << temp->block_num << "\t " << temp->size_KB << "\t\t " << temp->origin << "\t " << temp->end << "\t " << temp->state << endl;
		temp = temp->next;
	}
    cout<<endl<<"\t\t状态: 0 未分配 / 1 已分配"<<endl;
    cout << "------------------------------------------------------------------------------" << endl;
}
//申请时满足的存储块，在链表末尾插入一块足够的内存区域
void insert(pro_block *&last, int size)
{
	pro_block *pro_new = new pro_block[1];
	pro_new->block_num = last->block_num + 1;
	pro_new->origin = last->end + rand() % 200;
	pro_new->end = pro_new->origin + size;
	pro_new->size_KB = size;
	pro_new->state = 1;
	pro_new->next = NULL;
	last->next = pro_new;
    last = last->next;
}
//块太大，对其分割，高地址往低地址分割，取出大小合适的块，余下的低地址部分留在链表中
void cut(pro_block *&now, int size)
{
	pro_block *pro_new = new pro_block[1];
	pro_new->next = now->next;
	now->next = pro_new;
	pro_new->block_num = now->block_num + 1;
	pro_new->end = now->end;
	now->end = now->end - size;
	pro_new->origin = pro_new->end - size;
	pro_new->size_KB = size;
    now->size_KB = now->size_KB - size;
	pro_new->state = 1;
	pro_block *temp = pro_new->next;
	while (temp != NULL)
	{
		temp->block_num++;
		temp = temp->next;
	}
}
//块刚好合适，分配给申请进程
void allocation(pro_block *&now)
{
	now->state = 1;
}
//随机生成储存块信息，构建储存块链表
void create_table(pro_block *&first, pro_block *&last)
{
	srand(time(0));
	pro_block *temp = first, *t = first;
	int i = 1;
	while (i <= num)
	{
        if(i == 1)
		{
            temp->block_num = i++;
		    do{temp->size_KB = rand() % 20;}while(temp->size_KB == 0);
		    temp->origin = rand() % (1024 - temp->size_KB);
		    temp->end = temp->origin + temp->size_KB;
		    temp->state = rand() % 2;
		    last = temp;
		    pro_block *temp_1 = new pro_block[1];
		    temp->next = temp_1;
		    temp = temp->next;
        }
        else{
            temp->block_num = i++;
		    do{temp->size_KB = rand() % 20;}while(temp->size_KB == 0);
		    temp->origin = t->end + rand() % (256 - temp->size_KB);
		    temp->end = temp->origin + temp->size_KB;
		    temp->state = rand() % 2;
		    last = temp;
		    pro_block *temp_1 = new pro_block[1];
		    temp->next = temp_1;
		    temp = temp->next;
            t = t->next;
        }
	}
    delete last->next;
    last->next = NULL;
}
//按地址由低到高将存储块链表排序
void sort(pro_block *&first)
{
    pro_block *a = first;
    while(a != NULL)
    {
        pro_block *b = a;
        while(b != NULL)
        {
            if(b->origin < a->origin)
            {
                swap(a->origin,b->origin);
                swap(a->size_KB,b->size_KB);
                swap(a->end,b->end);
                swap(a->state,b->state);
            }
            b = b->next;
        }
        a = a->next;
    }
}
//循环首次适应算法
void NF(pro_block *&first, pro_block *&last, pro_block *&now, pro_block *&pos, int size)
{
	pro_block *temp = now;
	while (now != NULL)
	{
        if(now->state != 1)
		{
            if (size < now->size_KB)
		    {
			    cut(now, size); pos = now->next; return;
		    }
		    else if (size == now->size_KB)
		    {
			    allocation(now); pos = now; now = now->next; return;
		    }
        }
        now = now->next;
	}
	if (temp == first)
	{
		insert(last, size); pos = last; return;
	}
	if (now == NULL && temp != first)
	{
		now = first;
		while (now != temp)
		{
            if(now->state != 1)
			{   
                if (size < now->size_KB)
			    {
				    cut(now, size); pos = now->next; return;
			    }
			    else if (size == now->size_KB)
			    {
				    allocation(now); pos = now; now = now->next; return;
			    }
            }
            now = now->next;
		}
		if (now == temp)
		{
			insert(last, size); pos = last; return;
		}
	}
}
//首次适应算法
void FF(pro_block *&first, pro_block *&last, pro_block *&pos, int size)
{
    pro_block *temp = first;
    while(temp != NULL)
    {
        if(temp->state != 1)
        {
            if(size < temp->size_KB)
            {
                cut(temp, size);pos = temp->next;return;
            }
            else if(size == temp->size_KB)
            {
                allocation(temp);pos = temp;return;
            }
        }
        temp = temp->next;
    }
    if(temp == NULL)
    {
        insert(last, size);
        pos = last;
    }
}
//最佳适应算法 按储存块链表大小由小到大排序
void sort_BF(pro_block *&first)
{
    pro_block *a = first;
    while(a != NULL)
    {
        pro_block *b = a;
        while(b != NULL)
        {
            if(b->size_KB < a->size_KB)
            {
                swap(a->origin,b->origin);
                swap(a->size_KB,b->size_KB);
                swap(a->end,b->end);
                swap(a->state,b->state);
            }
            b = b->next;
        }
        a = a->next;
    }
}
//最佳适应算法
void BF(pro_block *&first, pro_block *&last, int &address, int size)
{
    pro_block *temp = first;
    while(temp != NULL)
    {
        if(temp->state != 1)
        {
            if(size < temp->size_KB)
            {
                cut(temp, size);address = temp->next->end; sort(first);return;
            }
            else if(size == temp->size_KB)
            {
                allocation(temp);address = temp->end; sort(first);return;
            }
        }
        temp = temp->next;
    }
    if(temp == NULL)
    {
        sort(first);
        insert(last, size);
        address = last->end;
        return;
    }
}
//最坏适应算法 按储存块链表大小由大到小排序
void sort_WF(pro_block *&first)
{
    pro_block *a = first, *temp = first;
    while(a != NULL)
    {
        pro_block *b = a;
        while(b != NULL)
        {
            if(b->size_KB > a->size_KB)
            {
                swap(a->origin,b->origin);
                swap(a->size_KB,b->size_KB);
                swap(a->end,b->end);
                swap(a->state,b->state);
            }
            b = b->next;
        }
        a = a->next;
    }
}
//最坏适应算法
void WF(pro_block *&first, pro_block *&last, int &address, int size)
{
    pro_block *temp = first;
    while(temp != NULL)
    {
        if(temp->state != 1)
        {
            if(size < temp->size_KB)
            {
                cut(temp, size);address = temp->next->end; sort(first);return;
            }
            else if(size == temp->size_KB)
            {
                allocation(temp);address = temp->end; sort(first);return;
            }
            else if(size > temp->size_KB)
            {
                sort(first);insert(last,size); address = last->end; return;
            }
        }
        temp = temp->next;
    }
}
//输出菜单
void display_meanu()
{
    printf("****************************\n");
        printf("1.请求分配内存\n2.释放进程储存块\n3.输出分区说明表\n4.输出已使用的储存块表\n5.输出进程表\nq.退出程序\n");
        printf("****************************\n");
}
//根据生成的储存块链表 生成进程链表
void process_create(Process *&pro_head, Process *&pro_last, pro_block *&first)
{
    int i =1;
    Process *t = pro_head, *a;
    pro_block *temp = first;
    while(temp != NULL)
    {
        if(temp->state == 1)
        {
            t->memory_pro = temp;
            t->num = i++;
            t->sz_kb = temp->size_KB;
            Process *temp1 = new Process[1];
            t->next = temp1;
            a = t;
            t = t->next;
        }
        temp = temp->next;
    }
    delete t;
    a->next = NULL;
    pro_last = a;
}
//添加进程分配储存块
void procee_add(Process *&pro_head, Process *&pro_last, pro_block *&pos)
{
    if(pro_head == NULL)
    {
        Process *temp = new Process[1];
        pro_head = temp;
        pro_head->num = 1;
        pro_head->sz_kb = pos->size_KB;
        pro_head->memory_pro = pos;
        pro_head->next = NULL;
        pro_last = pro_head;
        return;
    }
    if(pro_head->num == -1)
    {
        pro_head->num = 1;
        pro_head->sz_kb = pos->size_KB;
        pro_head->memory_pro = pos;
        pro_head->next = NULL;
        pro_last = pro_head;
        return;
    }
    else
    {
        Process *t = new Process[1];
        t->num = pro_last->num + 1;
        t->sz_kb = pos->size_KB;
        t->memory_pro = pos;
        t->next = NULL;
        pro_last->next = t;
        pro_last = pro_last->next;
    }
}
//查询进程的地址
void find_address(int a, pro_block *&first, pro_block *&pos)
{
    pro_block *temp = first;
    while(temp != NULL)
    {
        if(temp->end == a){pos = temp;return;}
        temp = temp->next;
    }
}
//输出进程表
void display_pro(Process *&pro_head)
{
    cout<<"进程列表"<<endl;
    if(pro_head == NULL){cout<<"空"<<endl; return;}
    if(pro_head->num == -1){cout<<"空"<<endl; return;}
    Process *temp = pro_head;
    cout << "------------------------------------------------------------------------------" << endl;
	cout << "\t进程号\t分区号\t大小(KB)\t起址(K)\t终址(K)\t状态" << endl;
    while(temp != NULL)
    {
        cout<<"\t"<<temp->num<<"\t"<<temp->memory_pro->block_num<<"\t"<<temp->memory_pro->size_KB
        <<"\t\t"<<temp->memory_pro->origin<<"\t"<<temp->memory_pro->end<<"\t"<<temp->memory_pro->state<<endl;
        temp = temp->next;
    }
}
//删除进程并释放占有的储存块
void delete_pro(int num, Process *&pro_head, Process *&pro_last, pro_block *&first)
{
    if(pro_head == NULL){cout<<"无此进程"<<endl;return;}
    else if(pro_head->num == -1){cout<<"空\n";return;}
    Process *temp = pro_head,*t = pro_head;
    int block_num;
    while(temp != NULL)
    {
        if(temp->num == num)
        {
            if(temp == pro_head)
            {
                block_num = temp->memory_pro->block_num;free_pro(block_num,first);
                if(pro_head->next == NULL){delete pro_head; pro_head = NULL; return;}
                else{
                    pro_head = pro_head->next;
                    t = pro_head;
                    delete temp; break;
                }
            }
            else if(pro_last != pro_head && pro_last == temp)
            {
                block_num = temp->memory_pro->block_num;free_pro(block_num,first);
                delete pro_last;
                pro_last = t;
                pro_last->next = NULL;
                return;
            }
            else
            {
                block_num = temp->memory_pro->block_num;
                free_pro(block_num,first);
                t->next = temp->next;
                delete temp;
                break;
            }
        }
        t = temp;
        temp = temp->next;
    }
    if(temp == NULL)
    {
        cout<<"无此进程"<<endl;
        char ch=getchar();
        ch = getchar();
        return;
    }
    if(t == pro_head){t->num=1;}
    Process *pnum = t->next;
    int i = 1;
    while(pnum != NULL)
    {
        pnum->num = t->num + i;
        pnum = pnum->next;
        i++;
    }
}

int main()
{
    pro_block *now, *last = NULL, *first = new pro_block[1];
    Process *pro_head = new Process[1], *pro_last;
	create_table(first, last);
    sort(first);
	now = first;
    put_table(first);
    process_create(pro_head,pro_last,first);
    display_pro(pro_head);
    char ch = ' ';
    while(ch != 'q')
    {
        
        if(ch == '1')
        {
            system("cls");
            char ma = ' ';
            put_table(first);
            int p_size;
            printf("1.首次适应 FF\n2.循环首次适应 NF\n3.最佳适应 BF\n4.最坏适应 WF\n");
            printf("请输入采用的算法：\n");
            do{scanf("%c",&ma);}while(ma != '1' && ma != '2' && ma != '3' && ma != '4');
            if(ma == '1')
            {
                system("cls");
                put_table(first);
                printf("输入进程需要的内存块大小\n");
                scanf("%d",&p_size);
                pro_block *pos;
                FF(first,last,pos,p_size);
                procee_add(pro_head,pro_last,pos);
            }
            else if(ma == '2')
            {
                system("cls");
                put_table(first);
                printf("输入进程需要的内存块大小\n");
                scanf("%d",&p_size);
                pro_block *pos;
                NF(first,last,now,pos,p_size);
                procee_add(pro_head,pro_last,pos);
            }
            else if(ma == '3')
            {
                system("cls");
                sort_BF(first);
                put_table(first);
                printf("输入进程需要的内存块大小\n");
                scanf("%d",&p_size);
                int pos_end;
                BF(first,last,pos_end,p_size);
                sort(first);
                pro_block *pos;
                find_address(pos_end,first,pos);
                procee_add(pro_head,pro_last,pos);
            }
            else if(ma == '4')
            {
                system("cls");
                sort_WF(first);
                put_table(first);
                printf("输入进程需要的内存块大小\n");
                scanf("%d",&p_size);
                int pos_end;
                WF(first,last,pos_end,p_size);
                sort(first);
                pro_block *pos;
                find_address(pos_end,first,pos);
                procee_add(pro_head,pro_last,pos);
            }
            put_table(first);
            display_pro(pro_head);
        }
        if(ch == '2')
        {
            system("cls");
            display_pro(pro_head);
            printf("输入要释放的进程号\n");
            int p_num;
            scanf("%d",&p_num);
            delete_pro(p_num,pro_head,pro_last,first);
            put_table(first);
            display_pro(pro_head);
        }
        if(ch == '3')
        {
            system("cls");
            put_table(first);
        }
        if(ch == '4')
        {
            system("cls");
            put_used(first);
        }
        if(ch == '5')
        {
            system("cls");
            display_pro(pro_head);
        }
        display_meanu();
        do{scanf("%c",&ch);}
        while(ch !='1' && ch !='2' && ch !='3' &&ch !='4' && ch != '5' && ch !='q');
    }
	return 0;
}