# Table of Contents

* [Scope](#scope)
  * [Hello World](#hello-world)
* [Languages](#languages)
  * [Elixir](#elixir)
  * [Ruby](#ruby)
  * [Nim](#nim)
  * [Node.js](#nodejs)
  * [Clojure](#clojure)
  * [Rust](#rust)
  * [Python](#python)
  * [Java](#java)
  * [Scala](#scala)
  * [GO](#go)
  * [Crystal](#crystal)
* [Benchmarks](#benchmarks)
  * [Platform](#platform)
  * [Wrk](#wrk)
  * [Results](#results)
  * [Plug with Cowboy](#plug-with-cowboy)
  * [Rack with Puma](#rack-with-puma)
  * [Nim asynchttpserver](#nim-asynchttpserver)
  * [Node Cluster](#node-cluster)
  * [Ring with Jetty](#ring-with-jetty)
  * [Rust Hyper](#rust-hyper)
  * [Gunicorn with Meinheld](#gunicorn-with-meinheld)
  * [Servlet3 with Jetty](#servlet3-with-jetty)
  * [Colossus](#colossus)
  * [GO ServeMux](#go-servemux)
  * [Crystal HTTP](#crystal-http)

## Scope
The idea behind this repository is to test how HTTP libraries for different languages behave under heavy loading.   

### Hello World
The "application" i tested is barely minimal: it is the HTTP version of the "Hello World" example.

## Languages
I chose to test the following languages/runtime:

### Elixir
[Elixir](http://elixir-lang.org/) 1.3.4 is installed via homebrew.  
I studied Elixir in 2015, surfing the wave of [Prag-Dave](https://pragdave.me/) enthusiasm and finding its *rubyesque* resemblance inviting.  
Being based on [Erlang](https://www.erlang.org/) it supports parallelism out of the box by spawning small (2Kb) processes.

### Ruby
[Ruby](https://www.ruby-lang.org/en/) 2.4 is installed via homebrew.  
Ruby is a scripting language focused on simplicity and productivity, inspired by SmallTalk.  
Ruby does support concurrency via native threads, but the standard MRI implementation uses an interpreter lock (GIL) to grant thread-safety. Parallelism is supported by pre-forking processes.

### Nim
[Nim](http://nim-lang.org/) 0.15.0 is installed from source.  
Nim is an efficient, Python inspired, strong typed language that comes with a pretty flexible compliler able to produce code in C (default), C++, JavaScript or ObjectiveC.  
Nim supports metaprogramming, functional, message passing, procedural, and object-oriented coding style.

### Node.js
[Node.js](https://nodejs.org/en/) version 7.0.1 is installed by official OSX package.  
Node.js is based on the V8 engine, optimized by Google and supporting most of the new ES6 features. Node.js leverages on the JavaScript built-in event loop to grant concurrency. Parallelism is supported via pre-forking.

### Clojure
[Clojure](http://clojure.org/) 1.8.0 is installed via homebrew.  
Clojure is a dynamic, general-purpose programming language, strongly inspired by Lisp, that runs on the JVM.  
Clojure is a compiled language, yet remains completely dynamic: every feature supported by Clojure is supported at runtime.

### Rust
[Rust](https://www.rust-lang.org/) language version 1.13.0 is installed by official OSX package.  
According to the official site Rust is a systems programming language that runs blazingly fast, prevents segfaults, and guarantees thread safety.  
Rust grants parallelism by running safely on multiple threads courtesy of its pretty unique ownership model.

### Python
[Python](https://www.python.org/) 3.5 is installed by official OSX package.  
Python is a widely used high-level, general-purpose, interpreted, dynamic programming language.  
It supports several programming paradigms and can count on a broad standard library.

### Java
[Java](https://www.java.com/en/) 8 comes pre-installed on Xcode 7.31.  
I get two Sun certifications back in 2006 and realized the more i delved into Java the less i liked it.
Ignoring Java on this comparison is not an option anyway: Java is the most used programming language in the world (2016) and some smart folks have invested on it since the 90ies.

### Scala
[Scala](https://www.scala-lang.org/) 2.12 and [SBT](http://www.scala-sbt.org/) 0.13 are installed via homebrew.  
Scala is a general-purpose programming language that runs on the JVM. It has full support for  functional, object oriented programming and a strong static type system.  
Designed to be concise, many of Scala's design decisions were inspired by criticism of Java's shortcomings.

### GO
[GO](https://golang.org/) language version 1.7.5 is installed by official OSX package.  
GO focuses on simplicity by intentionally lacking features considered redundant (an approach i am a fan of). It tries to address verbosity by using type inference, duck typing and a dry syntax.  
At the same time GO takes a straight approach to parallelism, coming with built in [CSP](https://en.wikipedia.org/wiki/Communicating_sequential_processes) and green threads (goroutines).  

### Crystal
[Crystal](http://crystal-lang.org/) 0.20.5 is installed via homebrew.  
Crystal has a syntax very close to Ruby, but brings some desirable features such as strong typing (hidden by a pretty smart type inference algorithm) and ahead of time compilation.  
For concurrency Crystal adopts the CSP model (like GO) and evented/IO to avoid blocking calls, but parallelism is not yet supported.

## Benchmarks
I decided to test each language by using the standard/built-in HTTP library, relying on external dependencies only when mandatory (Rust).

### Platform
I registered these benchmarks with a MacBook PRO 15 mid 2015 having these specs:
* OSX El Captain
* 2,2 GHz Intel Core i7 (4 cores)
* 16 GB 1600 MHz DDR3

### RAM and CPU
I measured RAM and CPU consumption by using the Apple XCode's Instruments and recording max consumption peak.  
Since both Ruby and Node starts multiple processes (8) i reported the total sum of RAM consumption and CPU usage.

### Wrk
I used [wrk](https://github.com/wg/wrk) as the loading tool.
I measured each application server three times, picking the best lap (apart for JVM that demands warm-up).  
Here is the common script i used:

```
wrk -t 4 -c 100 -d30s --timeout 2000 http://127.0.0.1:9292
```

### Results
Here are the benchmarks results ordered by increasing throughput.

| App Server                                        | Throughput (req/s) | Latency in ms (avg/stdev/max) | Memory (MB) |       %CPU |
| :------------------------------------------------ | -----------------: | ----------------------------: | ----------: | ---------: |
| [Plug with Cowboy](#plug-with-cowboy)             |          43653.65  |           10.86/18.94/249.45  |      48.29  |     438.1  |
| [Rack with Puma](#rack-with-puma)                 |          53747.65  |               0.28/0.47/5.28  |       ~230  |      ~420  |
| [Nim asynchttpserver](#nim-asynchttpserver)       |          70646.89  |              1.42/0.44/43.32  |       7.15  |      99.9  |
| [Node Cluster](#node-cluster)                     |          77035.04  |              1.50/1.82/93.68  |       ~316  |      ~551  |
| [Ring with Jetty](#ring-with-jetty)               |          77258.65  |              1.63/3.21/78.92  |     127.30  |     558.7  |
| [Rust Hyper](#rust-hyper)                         |          84493.74  |               1.18/0.13/3.73  |      27.71  |     350.4  |
| [Gunicorn with Meinheld](#gunicorn-with-meinheld) |          85655.09  |               1.16/0.21/9.33  |        ~72  |      ~349  |
| [Servlet3 with Jetty](#servlet3-with-jetty)       |          85709.89  |              1.16/0.21/15.61  |     144.90  |     427.3  |
| [Colossus](#colossus)                             |          90457.77  |               1.10/0.15/4.11  |     610.88  |     294.2  |
| [GO ServeMux](#go-servemux)                       |          92355.89  |               1.07/0.17/9.37  |       8.75  |     291.2  |
| [Crystal HTTP](#crystal-http)                     |         115968.64  |               0.86/0.14/9.61  |       9.02  |     112.4  |

### Plug with Cowboy
I tested Elixir by using [Plug](https://github.com/elixir-lang/plug) library that provides a [Cowboy](https://github.com/ninenines/cowboy) adapter.

##### Bootstrap
```
MIX_ENV=prod mix compile
MIX_ENV=prod mix run --no-halt
```

##### Considerations
Elixir performance are not stellar. To be fair the BEAM VM is not famous to be fast, but to grant reliability and resilience over a distributed system.  
Memory consumption is good, thanks to the fact that only one process is created.

##### Concurrency and parallelism
Elixir VM distributes the workloads on all of the available cores, thus supporting parallelism quite nicely.  

### Rack with Puma
I tested Ruby by using a plain [Rack](http://rack.github.io/) application with the [Puma](http://puma.io/) application server.  

##### Bootstrap

```
bundle exec puma -w 7 -t 0:2 app.ru
```

##### Considerations
Ruby delivers solid performance, with very good latency.  
Memory consumption is pretty high (~30MB per process).  

##### Concurrency and parallelism
Because of MRI's GIL, Puma relies on the pre-forking model for parallelism: 8 processes are forked (one as supervisor), each spawning multiple threads (which i limited to get better throughput).

### Nim asynchttpserver
I used the Nim asynchttpserver module to implement a high performance asynchronous server.  
Nim's asyncdispatch library is hard to use with threads, so the server runs on a single core only.  

##### Bootstrap
```
nim cpp -d:release nim_server.nim
./nim_server
```

##### Considerations
Nim proved to keep its promises, being a fast and concise language.  
Memory consumption is the smallest of the pack: unsurprisingly, considering Nim executes on a single thread.

##### Concurrency and parallelism
As expected Nim asynchttpserver is not parallel by implementation.

### Node Cluster
I used Node cluster library to spawn multiple processes, thus granting parallelism.

##### Bootstrap
```
node node_server.js
```

##### Considerations
JavaScript V8 on Node.js proved to be pretty fast, getting close to compiled languages.  
Node.js has the worst memory footprint of the pack (~40MB per process).  

##### Concurrency and parallelism
Node relies on the reactor pattern to grant non-blocking calls and uses the pre-forking model to get parallelism.

### Ring with Jetty
I used the default library to interface Clojure with HTTP: the [Ring](https://github.com/ring-clojure/ring) library.

##### Bootstrap
```
lein run
```

##### Considerations
Ring runs on the Jetty server, thus there is no surprise it is quite close to Java throughput but for some additional burden imposed by additional allocations.  
Memory footprint is also pretty the same.

##### Concurrency and parallelism
Clojure leverages on the JVM to deliver parallelism.

### Rust Hyper
Rust does not include (yet) an HTTP server into its standard library, so i picked one of the more mature micro-framework available: [Hyper](http://hyper.rs/). 

##### Bootstrap
```
cargo build --release
cargo run --release
```

##### Considerations
As expected Rust proved to be a very fast languages, although its memory consumption is larger than other binary-compiling languages.

##### Concurrency and parallelism
As expected Rust makes use of every available cores. 

### Gunicorn with Meinheld
I started a plain WSGI application on the [Gunicorn](http://gunicorn.org/) application server wrapping [Meinheld](http://meinheld.org/) workers. 

##### Bootstrap
```
gunicorn -w 8 gunicorn_server:app -b :9292 -k meinheld.gmeinheld.MeinheldWorker
```

##### Considerations
Gunicorn and Meinheld combination is blazing fast, surpassing even some compiled languages.  
Memory footprint is average, considering Gunicorn pre-forks eight processes (~7MB per process). 

##### Concurrency and parallelism
Gunicorn relies on the pre-forking model to grant parallelism.

### Servlet3 with Jetty
To test Java i used [Jetty](http://www.eclipse.org/jetty/): a modern, stable and quite fast servlet container.  

##### Bootstrap
```
javac -cp javax.servlet-3.0.0.v201112011016.jar:jetty-all-9.2.14.v20151106.jar HelloWorld.java
java -server -cp .:javax.servlet-3.0.0.v201112011016.jar:jetty-all-9.2.14.v20151106.jar HelloWorld
```

##### Considerations
I know Java is pretty fast nowadays: thousands of optimizations have been done to the JVM during the last two decades.  
Memory footprint of the JVM is high, at least compared to other VM (i.e. BEAM).

##### Concurrency and parallelism
JVM allows Java to use all of the available cores.  

### Colossus
To test Scala i used [Colossus](http://tumblr.github.io/colossus/): a lightweight framework for building high-performance network I/O applications in Scala.

##### Bootstrap
```
sbt
> compile
> run
```

##### Considerations
Scala in combination with [Akka](http://akka.io/) (the toolkit on which Colossus is build) proves to be pretty performant.  
Unfortunately memory footprint is the worst of the pack.

##### Concurrency and parallelism
JVM allows Scala to use all of the available cores.  

### GO ServeMux
I opted for the [HTTP ServeMux](https://golang.org/pkg/net/http/) GO standard library.  
I also tested [fast/http](https://github.com/valyala/fasthttp) library: it proved to be faster than ServerMux but its interface is not as readable and the idea is to stick with the language standard library when possible.

##### Bootstrap
```
go build go_server.go
./go_server
```

##### Considerations
GO is a pretty fast language and allows using all of the cores with no particular configuration.  
Memory consumption and resiliency are really good.

##### Concurrency and parallelism
GO runs natively on all of the cores: indeed it seems to be a little conservative on CPUs percentage usage.  

### Crystal HTTP
I used Crystal HTTP server standard library.  
Crystal uses green threads, called "fibers", spawned inside an event loop via the [libevent](http://libevent.org/) library.

##### Bootstrap
```
crystal build --release ./server/crystal_server.cr
./crystal_server
```

##### Considerations
Crystal language recorded the best lap of the pack, outperforming more mature languages.  
Memory consumption and resiliency are also very good.

##### Concurrency and parallelism
As expected Crystal does not supports parallelism yet.
