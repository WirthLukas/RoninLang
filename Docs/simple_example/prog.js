/*      Ronin Extensions        */

Object.prototype.also = function(exec) {
    exec(this);
    return this;
}

function Ref(read, write) {
    this.read = read;
    this.write = write;
}

Ref.prototype = {
    get value() { return this.read(); },
    set value(v) { return this.write(v); }
}

const ref = (read, write) => {
    // return {
    //     get value() { return read(); },
    //     set value(v) { return write(v); }
    // };
    return new Ref(read, write);
};

const alloc = () => {
    let item;
    return ref(() => item, v => item = v);
}

/*      Types       */

function Node(item, next) {
    this.item = item;
    this.next = next;
}

function List(start) {
    this.start = start;
}

Node.prototype = {
    isLast: function() {
        return this.next === undefined;
    }
} 

List.prototype = {
    isEmpty: function() {
        return this.start === undefined;
    },
    add: function(item) {
        const newNode = alloc().also(r => r.value = new Node(item));
    
        if (this.isEmpty()) {
            this.start = newNode;
            return;
        }
    
        let current = this.start.value;
    
        while(!current.isLast()) current = current.next;
    
        current.next = newNode;
    },
    forEach: function(exec) {
        if (this.isEmpty()) return;
    
        let current = this.start;
    
        do {
            exec(current.value.item);
            current = current.value.next;
        } while(current !== undefined);
    }
}

/*      Prog        */

const main = () => {
    console.log("Test prog");
    const list = new List();

    list.add(10);
    list.add(20);
    list.forEach(i => console.log(i));
}

main();